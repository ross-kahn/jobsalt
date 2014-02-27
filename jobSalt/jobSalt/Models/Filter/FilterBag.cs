using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models;
using jobSalt.Models.Data_Types;
using System.Web.Script.Serialization;

namespace jobSalt.Models
{
    /// <summary>
    /// For release 1, any given filter may have a single value, or none at all.
    /// It makes sense, then, to have a single data structure to hold all the
    /// filter information for a single request from a user.
    /// </summary>
    public class FilterBag
    {
        //public enum Field { Source, Date, CompanyName, JobTitle, Location, Salary, FieldOfStudy, Keyword, EducationCode};

        private FilterBag() { }

        public static FilterBag createFromJSON(string json)
        {
            var serializer = new JavaScriptSerializer();
            FilterBagResult fbag = serializer.Deserialize<FilterBagResult>(json);

            FilterBag filterBag = new FilterBag();

            foreach (Filter f in fbag.filters)
            {
                switch (f.TargetField)
                {
                    case Field.CompanyName:
                        filterBag.companyName = f.Value;
                        break;

                    case Field.FieldOfStudy:
                        filterBag.fieldOfStudy = f.Value;
                        break;

                    case Field.JobTitle:
                        filterBag.jobTitle = f.Value;
                        break;

                    case Field.Keyword:
                        filterBag.keyword = f.Value;
                        break;

                    case Field.Location:
                        string decoded = System.Web.HttpUtility.UrlDecode(f.Value);
                        filterBag.location = serializer.Deserialize<Location.LocationResult>(decoded).location;
                        break;
                }
            }
            return filterBag;
        }

        public Location location { get; private set; }
        public string companyName { get; private set; }
        public string jobTitle { get; private set; }
        public string fieldOfStudy { get; private set; }
        public string keyword { get; private set; }
        public class FilterBagResult{public List<Filter> filters { get; set; }  }


    }
}