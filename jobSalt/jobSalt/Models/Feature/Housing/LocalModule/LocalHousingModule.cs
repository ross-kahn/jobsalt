using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Feature.Housing.LocalModule
{
    public class LocalHousingModule : IHousingModule
    {
        public Data_Types.Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<string, List<HousingPost>> GetHousing(FilterBag filters)
        {
            
            return null;
        }
    }
}