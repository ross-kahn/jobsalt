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
                 Rating = (int)review.Rating
             }).ToList();
            
            return posts;
        }
    }
}