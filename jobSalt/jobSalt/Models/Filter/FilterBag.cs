using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models;

namespace jobSalt.Filters
{
    public class FilterBag
    {

        private FilterBag() { }

        public static FilterBag createFromJSON(string json)
        {
            return new FilterBag();
        }

        //public Location location {get;}


   

    
    }
}