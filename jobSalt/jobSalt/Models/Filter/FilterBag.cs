using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobSalt.Models;
using jobSalt.Models.Data_Types;
using System.Web.Script.Serialization;

namespace jobSalt.Models
{
    public enum Field { Source, Date, CompanyName, JobTitle, Location, Salary, FieldOfStudy, Keyword, EducationCode };

    /// <summary>
    /// A single data structure to hold all the
    /// filter information for a single request from a user.
    /// </summary>
    public class FilterBag
    {
        private Dictionary<Field, string> filters = new Dictionary<Field, string>();

        private FilterBag() { }

        public static FilterBag createFromJSON(string json)
        {
            if (String.IsNullOrWhiteSpace(json)) { return new FilterBag(); }


            var serializer = new JavaScriptSerializer();
            List<Filter> filtersList = serializer.Deserialize<List<Filter>>(json);
            FilterBag filterBag = new FilterBag();

            if (filtersList == null || String.IsNullOrWhiteSpace(json))
            {
                return filterBag;
            }

            foreach (Filter f in filtersList)
            {
                filterBag.filters[f.TargetField] = f.Value;
            }
            return filterBag;

        }

        public void AssignFilter(Filter filter)
        {
            filters[filter.TargetField] = filter.Value;
        }

        public void RemoveFilter(Field field)
        {
            if (filters.ContainsKey(field))
            {
                filters.Remove(field);
            }
        }

        public Type Deserialize<Type>(string value)
        {
            var serializer = new JavaScriptSerializer();
            string decodedValue = System.Web.HttpUtility.UrlDecode(value);

            return  (Type)serializer.Deserialize<Type>(decodedValue);
        }

        public List<Filter> GetFilters(bool URLEncoded = false)
        {
            List<Filter> filterList = new List<Filter>();

            foreach(Field key in filters.Keys)
            {
                filterList.Add(new Filter(key, GetFilterValue(key)));
            }

            return filterList;
        }

        public string GetFilterValue(Field target)
        {
            switch(target)
            {
                case Field.Location:
                    return this.Location.ToString();
                case Field.CompanyName:
                    return this.CompanyName;
                case Field.FieldOfStudy:
                    return this.FieldOfStudy;
                case Field.JobTitle:
                    return this.JobTitle;
                case Field.Keyword:
                    return this.Keyword;
                default:
                    return "";
            }
        }

        public string JsonEncode()
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(GetFilters(true));
            json = json.Replace("\"", "'");
            return json;
        }

        public Location Location 
        {
            get { return filters.ContainsKey(Field.Location) ? Deserialize<Location>(filters[Field.Location]) : null; } 
        }
        public string CompanyName 
        {
            get { return filters.ContainsKey(Field.CompanyName) ? filters[Field.CompanyName] : ""; } 
        }
        public string JobTitle 
        {
            get { return filters.ContainsKey(Field.JobTitle) ? filters[Field.JobTitle] : ""; }
        }
        public string FieldOfStudy 
        {
            get { return filters.ContainsKey(Field.FieldOfStudy) ? filters[Field.FieldOfStudy] : ""; }
        }
        public string Keyword 
        {
            get { return filters.ContainsKey(Field.Keyword) ? filters[Field.Keyword] : ""; }
        }

    }
}