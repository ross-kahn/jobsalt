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
    /// A single data structure to hold all the
    /// filter information for a single request from a user.
    /// </summary>
    public class FilterBag
    {
        //public enum Field { Source, Date, CompanyName, JobTitle, Location, Salary, FieldOfStudy, Keyword, EducationCode};

        private FilterBag() { }

        public static FilterBag createFromJSON(string json)
        {
            var serializer = new JavaScriptSerializer();
            FilterBag fbag = serializer.Deserialize<FilterBag>(json);

            return fbag;
        }

        public Location location { get; set; }
        public string companyName { get; set; }
        public string jobTitle { get; set; }
        public string fieldOfStudy { get; set; }
        public string keyword { get; set; }
    }
}