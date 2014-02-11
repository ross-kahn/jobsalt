using jobSalt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    /// <summary>
    /// This class is used across other controllers and views to modify
    /// the filters. An API is exposed through the FilterUtlityController to add/remove/edit
    /// filters stored in the filterString.
    /// </summary>
    public class FilterUtility
    {
        public static string AssignFilter(Field targetField, string value, string filterString)
        {
            Dictionary<Field, string> filters = Filter.FilterQueryStringToDictionary(filterString);

            if(filters.ContainsKey(targetField))
            {
                filters[targetField] = value;
            }
            else
            {
                filters.Add(targetField, value);
            }
            return Filter.FilterListToUrlQueryString(filters);
        }

        public static string RemoveFilter(Field targetField, string filterString)
        {
            Dictionary<Field, string> filters = Filter.FilterQueryStringToDictionary(filterString);

            filters.Remove(targetField);

            return Filter.FilterListToUrlQueryString(filters);
        }

        public static List<Models.Filter> GetFilters(string filterString)
        {
            Dictionary<Field, string> filterDict = Filter.FilterQueryStringToDictionary(filterString);
            List<Models.Filter> filters = new List<Models.Filter>();
            foreach( Field target in filterDict.Keys)
            {
                filters.Add(new Models.Filter(target, filterDict[target]));
            }

            return filters;
        }

        public static string GetFilterValue(Field targetField, string filterString)
        {
            Dictionary<Field, string> filters = Filter.FilterQueryStringToDictionary(filterString);

            if(filters.ContainsKey(targetField))
            {
                return filters[targetField];
            }
            else
            {
                return null;
            }
        }
    }
}
