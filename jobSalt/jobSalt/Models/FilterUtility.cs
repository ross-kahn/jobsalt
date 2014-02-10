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
    /// filters stored in the user's session.
    /// </summary>
    public class FilterUtility
    {
        public static void AssignFilter(Field targetField, string value)
        {
            InitializeSessionVariable();
            Dictionary<Field, string> filters = HttpContext.Current.Session["Filters"] as Dictionary<Field, string>;

            if(filters.ContainsKey(targetField))
            {
                filters[targetField] = value;
            }
            else
            {
                filters.Add(targetField, value);
            }

            HttpContext.Current.Session["Filters"] = filters;
        }

        public static void RemoveFilter(Field targetField)
        {
            InitializeSessionVariable();
            Dictionary<Field, string> filters = HttpContext.Current.Session["Filters"] as Dictionary<Field, string>;

            filters.Remove(targetField);

            HttpContext.Current.Session["Filters"] = filters;
        }

        public static List<Models.Filter> GetFilters()
        {
            InitializeSessionVariable();
            var filterDict = HttpContext.Current.Session["Filters"] as Dictionary<Field, string>;
            List<Models.Filter> filters = new List<Models.Filter>();
            foreach( Field target in filterDict.Keys)
            {
                filters.Add(new Models.Filter(target, filterDict[target]));
            }

            return filters;
        }

        public static string GetFilterValue(Field targetField)
        {
            InitializeSessionVariable();
            Dictionary<Field, string> filters = HttpContext.Current.Session["Filters"] as Dictionary<Field, string>;

            if(filters.ContainsKey(targetField))
            {
                return filters[targetField];
            }
            else
            {
                return null;
            }
        }

        private static void InitializeSessionVariable()
        {
            if (HttpContext.Current.Session["Filters"] == null)
            {
                HttpContext.Current.Session["Filters"] = new Dictionary<Field, string>();
            }
        }
    }
}
