using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Housing.LocalModule
{
    public class LocalHousingModule : IHousingModule
    {
        
        HousingDBEntities dbConext = new HousingDBEntities();
        public Data_Types.Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public LocalHousingModule()
        {
            bool releaseConfiguration = true;

            #if DEBUG
                releaseConfiguration = false;
            #endif

            if (releaseConfiguration)
                dbConext.ChangeDatabase(Config.ConfigLoader.SiteConfig.HousingDBConnection);
        }

        public List<HousingPost> GetHousing(FilterBag filters, int page, int resultsPerPage)
        {
            var query = dbConext.HousingReviews.AsQueryable();
            if(!String.IsNullOrEmpty(filters.Keyword))
            {
                query = query.Where(review =>
                    review.Description.Contains(filters.Keyword) ||
                    review.Title.Contains(filters.Keyword));
            }

            List<HousingPost> reviews = query.Select(review => new HousingPost()
              {
                  ID = review.Id,
                  Title = review.Title,
                  DatePosted = (DateTime)review.DateTime,
                  Description = review.Description,
                  Rating = (int)review.Rating,
                  Location = new Data_Types.Location()
                  {
                      City = review.HousingLocation.City,
                      State = review.HousingLocation.State,
                      ZipCode = review.HousingLocation.ZipCode,
                      Longitude = review.HousingLocation.Longitude ?? 0,
                      Latitude = review.HousingLocation.Latitude ?? 0
                  },
                  PostedBy = review.SubmittedBy
              }).ToList();

            if(filters.Location != null)
            {
                reviews = reviews.Where(review =>
                {
                    double lat1 = (Math.PI / 180) * filters.Location.Latitude;
                    double lat2 = (Math.PI / 180) * review.Location.Latitude;
                    double lon1 = (Math.PI / 180) * filters.Location.Longitude;
                    double lon2 = (Math.PI / 180) * review.Location.Longitude;
                    double dlat = lat2 - lat1;
                    double dlon = lon2 - lon1;
                    double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                               Math.Cos(lat1) *
                               Math.Cos(lat2) *
                               Math.Pow(Math.Sin(dlon / 2), 2);
                    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    double dist = 3961 * c;
                    return dist < 50;
                }).ToList();
            }

            reviews = reviews.OrderByDescending(review => review.Rating).ToList();
            reviews = reviews.Skip(page * resultsPerPage).Take(resultsPerPage).ToList();
            
            return reviews;
        }

        public void AddHousingPost(HousingPost post, string submittedBy)
        {
            DateTime submitedTime = DateTime.Now;
            dbConext.HousingReviews.Add(
                new HousingReview()
                {
                    Title = post.Title,
                    DateTime = DateTime.Now,
                    Description = post.Description,
                    Price = post.PriceRange,
                    Rating = post.Rating,
                    HousingLocation = new HousingLocation()
                    {
                        City = post.Location.City,
                        State = post.Location.State,
                        ZipCode = post.Location.ZipCode,
                        Longitude = post.Location.Longitude,
                        Latitude = post.Location.Latitude
                    },
                    SubmittedBy = submittedBy
                });
            dbConext.SaveChanges();
        }

        public void DeleteHousingPost(int postID)
        {
            HousingReview review = dbConext.HousingReviews.Find(postID);
            if(review != null)
            {
                dbConext.HousingReviews.Remove(review);
                dbConext.SaveChanges();
            }
        }

        public HousingPost GetHousingPost(int postID)
        {
            var review = dbConext.HousingReviews.Find(postID);
            if(review != null)
            {
                return new HousingPost()
                 {
                     ID = review.Id,
                     Title = review.Title,
                     DatePosted = (DateTime)review.DateTime,
                     Description = review.Description,
                     Rating = (int)review.Rating,
                     Location = new Data_Types.Location()
                     {
                         City = review.HousingLocation.City,
                         State = review.HousingLocation.State,
                         ZipCode = review.HousingLocation.ZipCode,
                         Longitude = review.HousingLocation.Longitude ?? 0,
                         Latitude = review.HousingLocation.Latitude ?? 0
                     },
                     PostedBy = review.SubmittedBy
                 };
            }
            else
            {
                return null;
            }
        }
    }
}