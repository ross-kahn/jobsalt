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
            List<Filter> filters = serializer.Deserialize<List<Filter>>(json);
            FilterBag filterBag = new FilterBag();

            if(filters == null || String.IsNullOrWhiteSpace(json))
            {
                return filterBag;
            }

            foreach (Filter f in filters)
            {
                filterBag.AssignFilter(f);
            }
            return filterBag;

        }

        public bool AssignFilter(Filter filter)
        {
            var serializer = new JavaScriptSerializer();
            string propName = GetPropertyName(filter.TargetField);
            if (propName == null)
            {
                // We don't know what property this goes to... so we ignore it
                return false;
            }
            var prop = this.GetType().GetProperty(propName);
            if (prop == null)
            {
                // The property name was invalid... ignore it
                return false;
            }

            string decodedValue = System.Web.HttpUtility.UrlDecode(filter.Value);

            if (prop.PropertyType.IsPrimitive || typeof(String) == prop.PropertyType)
            {
                // property to set.SetValue(Object of property, value to set it to, optional index)
                prop.SetValue(this, decodedValue, null);
            }
            else
            {
                object value = serializer.Deserialize(decodedValue, prop.GetType());
                prop.SetValue(this, value, null);
            }
            return true;
        }

        public List<Filter> GetFilters(bool URLEncoded = false)
        {
            List<Filter> filters = new List<Filter>();

            string value = "";

            if(Location != null)
            {
                value = String.Join(", ", Location.City, Location.State, Location.ZipCode);
                if (URLEncoded) { value = HttpUtility.UrlEncode(value); }
                filters.Add(new Filter(Field.Location, value));
            }
            if (!String.IsNullOrEmpty(CompanyName))
            {
                value = CompanyName;
                if (URLEncoded) { value = HttpUtility.UrlEncode(value); }
                filters.Add(new Filter(Field.CompanyName, value));
            }
            if (!String.IsNullOrEmpty(JobTitle))
            {
                value = JobTitle;
                if (URLEncoded) { value = HttpUtility.UrlEncode(value); }
                filters.Add(new Filter(Field.JobTitle, value));
            }
            if (!String.IsNullOrEmpty(FieldOfStudy))
            {
                value = FieldOfStudy;
                if (URLEncoded) { value = HttpUtility.UrlEncode(value); }
                filters.Add(new Filter(Field.FieldOfStudy, value));
            }
            if (!String.IsNullOrEmpty(Keyword))
            {
                value = Keyword;
                if (URLEncoded) { value = HttpUtility.UrlEncode(value); }
                filters.Add(new Filter(Field.Keyword, value));
            }
            return filters;
        }

        public string JsonEncode()
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(GetFilters(true));
            return json;
        }

        public Location Location { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string FieldOfStudy { get; set; }
        public string Keyword { get; set; }


        private static string GetPropertyName(Field targetField)
        {
            switch (targetField)
            {
                case Field.CompanyName:
                    return "CompanyName";

                case Field.FieldOfStudy:
                    return "FieldOfStudy";

                case Field.JobTitle:
                    return "JobTitle";

                case Field.Keyword:
                    return "Keyword";

                case Field.Location:
                    return "Location";
                default:
                    return null;
            }
        }
    }
}