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
        public static List<Models.Filter> GetFilters(string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);

            return fb.GetFilters();
        }

        public static string GetFilterValue(Field targetField, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            return fb.GetFilterValue(targetField);
        }

        public static string AssignFilter(Field targetField, string value, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            fb.AssignFilter(new Models.Filter(targetField, value));
            string newFilterString = fb.JsonEncode().Replace("\"", "'");

            return newFilterString;
        }

        public static bool FilterIsSet(List<Filter> filters, Field targetField)
        {
            return filters.Find(filter => filter.TargetField == targetField) != null;
        }
    }
}
