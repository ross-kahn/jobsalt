using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobSalt.Models
{
    public enum Field { Source, Date, CompanyName, JobTitle, Location, Salary, Description, FieldOfStudy, Keyword};

    public class Filter
    {
        public Field TargetField { get; private set; }
        public string Value { get; private set; }

        public Filter(Field targetField, string value)
        {
            this.TargetField = targetField;
            this.Value = value;
        }

        public static Dictionary<Field, List<string>> FilterListToDictionary(List<Filter> filters)
        {
            // Create a dictionary with Key => FilterType and Value => The actual filter
            Dictionary<Field, List<string>> FilterHash = new Dictionary<Field, List<string>>();

            // Iterate through the list of filters and parse into the filter hash
            foreach (Filter filter in filters)
            {
                // If the key exists, add the corresponding filter to the end of the list for that type
                if (FilterHash.ContainsKey(filter.TargetField))
                {
                    FilterHash[filter.TargetField].Add(filter.Value);
                }
                else // Create a new list
                {
                    List<string> list = new List<string>();
                    list.Add(filter.Value);
                    FilterHash[filter.TargetField] = list;
                }
            }

            return FilterHash;
        }


    }
}