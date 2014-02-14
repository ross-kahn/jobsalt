using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace jobSalt.Models
{
    public enum Field { Source, Date, CompanyName, JobTitle, Location, Salary, FieldOfStudy, Keyword};

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

        /// <summary>
        /// Creates a partial url query string from a dictionary.
        /// This query string needs to be appended to the full URL. 
        /// For example, if "?filterStrings={Source,Indeed}{Keyword,Computers}" is returned,
        /// then the full URL could be "http://localhost:38087/Job?filterStrings={Source,Indeed}{Keyword,Computers}".
        /// </summary>
        /// <param name="filters">The filters to create a query string from.</param>
        /// <returns>A URL query string corresponding to the given list of filters.</returns>
        public static String FilterListToUrlQueryString(Dictionary<Field, string> filters)
        {
            //build the queryString
            StringBuilder queryString = new StringBuilder();

            //fill with each filter in the form of {filterTarget,value}
            foreach (Field key in filters.Keys)
            {
                queryString.Append("{" + key + "," + filters[key] + "}");
            }

            //return
            return queryString.ToString();
        }

        /// <summary>
        /// Takes a list of filters given in their query format and converts them into a 
        /// list of Filter objects.
        /// </summary>
        /// <param name="filterStrings">The filters query string to create a list from</param>
        /// <returns>A list of filters built off the query string</returns>
        public static List<Filter> FilterQueryStringToList(String filterStrings)
        {
            List<Filter> filters = new List<Filter>();

            if (filterStrings != null)
            {

                //fill filters with filters from filterStrings
                foreach (string filterString in filterStrings.Split(new Char[] { '{', '}' })) //split into individual filter strings first
                {
                    //split current filter string into a string array {targetFiled,value}
                    String[] currentFilterString = filterString.Split(new char[] { ',' }, 2);

                    if (currentFilterString.Length == 2 && Enum.IsDefined(typeof(Models.Field), currentFilterString[0]))
                    {

                        //get target field from Enum
                        Models.Field targetField = (Models.Field)Enum.Parse(typeof(Models.Field), currentFilterString[0]);

                        //build the current filter
                        Models.Filter filter = new Models.Filter(targetField, currentFilterString[1]);

                        //if the filter is not already in the filter list, add it.
                        if (!filters.Any(a => a.TargetField.Equals(filter.TargetField) && a.Value.Equals(filter.Value)))
                        {
                            filters.Add(filter);
                        }
                    }
                }
            }

            return filters;
        }

        /// <summary>
        /// Takes a list of filters given in their query format and converts them into a 
        /// list of Filter objects.
        /// </summary>
        /// <param name="filterStrings">The filters query string to create a list from</param>
        /// <returns>A list of filters built off the query string</returns>
        public static Dictionary<Field, string> FilterQueryStringToDictionary(String filterStrings)
        {
            Dictionary<Field, string> filters = new Dictionary<Field, string>();

            if (filterStrings != null)
            {

                //fill filters with filters from filterStrings
                foreach (string filterString in filterStrings.Split(new Char[] { '{', '}' })) //split into individual filter strings first
                {
                    //split current filter string into a string array {targetFiled,value}
                    String[] currentFilterString = filterString.Split(new char[] { ',' }, 2);

                    if (currentFilterString.Length == 2 && Enum.IsDefined(typeof(Models.Field), currentFilterString[0]))
                    {

                        //get target field from Enum
                        Models.Field targetField = (Models.Field)Enum.Parse(typeof(Models.Field), currentFilterString[0]);
                        string value = currentFilterString[1];

                        // Replace the value if the key already exists, otherwise add it.
                        if(filters.ContainsKey(targetField))
                        {
                            filters[targetField] = value;
                        }
                        else
                        {
                            filters.Add(targetField, value);
                        }
                    }
                }
            }

            return filters;
        }
    }
}