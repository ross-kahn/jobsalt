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

        public List<HousingPost> GetHousing(FilterBag filters)
        {
            List<HousingPost> posts = dbConext.HousingReviews.Select(review => new HousingPost()
              {
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
                  }
              }).ToList();
            
            return posts;
        }

        public void AddHousingPost(HousingPost post)
        {
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
                    }
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
    }
}