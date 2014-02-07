using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filters"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Creates a partial url query string from an IEnumerable list of filters.
		/// This query string needs to be appended to the full URL. 
		/// For example, if "?filterStrings={Source,Indeed}{Keyword,Computers}" is returned,
		/// then the full URL could be "http://localhost:38087/Job?filterStrings={Source,Indeed}{Keyword,Computers}".
		/// </summary>
		/// <param name="filters">The filters to create a query string from.</param>
		/// <returns>A URL query string corresponding to the given list of filters.</returns>
		public static String FilterListToUrlQueryString ( IEnumerable<Filter> filters )
			{
			//build the queryString
			StringBuilder queryString = new StringBuilder( );

			//fill with each filter in the form of {filterTarget,value}
			foreach ( Filter currentFilter in filters.Distinct() )
				{
				queryString.Append( "{"+currentFilter.TargetField+","+currentFilter.Value+"}" );
				}

			//return
            return queryString.ToString();
			}


    }
}