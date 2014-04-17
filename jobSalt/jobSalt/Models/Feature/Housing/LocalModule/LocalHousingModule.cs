using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Housing.LocalModule
{
    public class LocalHousingModule : IHousingModule
    {
        HousingDataEntities dbConext = new HousingDataEntities();
        public Data_Types.Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<string, List<HousingPost>> GetHousing(FilterBag filters)
        {
            Dictionary<string, List<HousingPost>> posts = new Dictionary<string, List<HousingPost>>();

            List<HousingPost> local = new List<HousingPost>();

            dbConext.Tables.Select(item => item.Id>0);

            posts.Add("local", local);
            return posts;
        }
    }
}