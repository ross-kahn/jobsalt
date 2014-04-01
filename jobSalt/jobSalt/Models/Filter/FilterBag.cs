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

        public static FilterBag createFromURLQuery(string query)
        {
            int index = query.IndexOf("filterString=", StringComparison.CurrentCultureIgnoreCase);
            string fs = index < 0 ? "" : query.Substring(index + 13);
            fs = HttpUtility.UrlDecode(fs);

            return createFromJSON(fs);
        }

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

        public object Deserialize<t>(string value)
        {
            var serializer = new JavaScriptSerializer();
            string decodedValue = FullyDecode(value);

            return serializer.Deserialize<t>(decodedValue);
        }

        private static string FullyDecode(string encodedString)
        {
            string decodedString = HttpUtility.UrlDecode(encodedString);
            string secondPass = HttpUtility.UrlDecode(decodedString);
            while(!decodedString.Equals(secondPass))
            {
                decodedString = secondPass;
                secondPass = HttpUtility.UrlDecode(secondPass);
            }

            return decodedString;
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

        public bool isEmpty()
        {
            return (GetFilters().Count == 0);
        }

        public string GetFilterValue(Field target)
        {
            switch(target)
            {
                case Field.Location:
                    return Location == null ? null : Location.ToString();
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
            List<Filter> filterList = new List<Filter>();

            foreach (Field key in filters.Keys)
            {
                // This seems really silly to fully decode the string and then re-encode it once.
                // But MVC is behind the scenes doing URL encoding to our parameters and so
                // if we don't do this we get ever componding url encoding.
                string decodedValue = FullyDecode(filters[key]);
                filterList.Add(new Filter(key, HttpUtility.UrlEncode(decodedValue)));
            }

            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(filterList);

            json = json.Replace("\"", "'");
            return json;
        }

        public Location Location 
        {
            get { return filters.ContainsKey(Field.Location) ? (Location)Deserialize<Location>(filters[Field.Location]) : null; } 
        }
        public string CompanyName 
        {
            get { return filters.ContainsKey(Field.CompanyName) ? HttpUtility.UrlDecode(filters[Field.CompanyName]) : ""; } 
        }
        public string JobTitle 
        {
            get { return filters.ContainsKey(Field.JobTitle) ? HttpUtility.UrlDecode(filters[Field.JobTitle]) : ""; }
        }
        public string FieldOfStudy 
        {
            get { return filters.ContainsKey(Field.FieldOfStudy) ? HttpUtility.UrlDecode(filters[Field.FieldOfStudy]) : ""; }
        }
        public string Keyword 
        {
            get { return filters.ContainsKey(Field.Keyword) ? HttpUtility.UrlDecode(filters[Field.Keyword]) : ""; }
        }

    }
}