using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace jobSalt.Models
{
    public class LinkedInQueryBuilder
    {
        // These should not change between users
        private const string FORMAT_TAG = "&format=json";
        private const string VERSION_TAG = "&v=2";
        
        // TODO: OBTAIN THESE FROM USER SESSION (hard-coded right now)
        private const string USER_IP = "1.2.3.4"; // 129.21.108.174
        private const string USER_AGENT = "Mozilla/%2F4.0%28Firefox%29";

        public string buildQuery(Dictionary<Field, string> filterHash, int page, int resultsPerPage)
        {

            // String builder, (arguably) more efficient than concatenating strings
            StringBuilder builder = new StringBuilder();

            // The required base for all requests
            builder.Append(Constants.LINKEDIN_REQUEST_BASE);
            builder.Append("&start=" + page * resultsPerPage);
            builder.Append("&limit=" + resultsPerPage);

            foreach (Field key in filterHash.Keys)
            {
                switch (key)
                {
                    case Field.CompanyName:  // Should be combined with keyword
                        builder.Append(companyName_converter(filterHash[Field.CompanyName]));
                        break;

                    case Field.Date:
                        break;

                    case Field.FieldOfStudy: // Should be combined with keyword
                        break;

                    case Field.JobTitle:    // Should be combined with keyword
                        break;

                    case Field.Keyword:
                        builder.Append(build_tag_query(filterHash[Field.Keyword]));
                        break;

                    case Field.Location:
                        builder.Append("&l=" + filterHash[key][0]);
                        break;

                    case Field.Salary:
                        break;

                    case Field.Source:
                        break;

                    default:
                        break;
                }
            }

            // Required tags
            builder.Append(FORMAT_TAG);                                 // The result comes back in JSON format
            builder.Append(build_tag_limit(Constants.RESULT_LIMIT));    // The limit of # of results returned
            builder.Append(build_tag_userip(USER_IP));                  
            builder.Append(build_tag_useragent(USER_AGENT));
            builder.Append(VERSION_TAG);

            return builder.ToString();
        }

        #region Tag String Builders

        private string build_tag_format(List<string> filterResults)
        {
            return "&format=json";
        }

        // Indeed supports advanced search, which (depending on how you wanted the search criteria)
        // would change how this string is formated for the request. For now, it's formatted in
        // the default way, which is putting a logical "AND" on each of the search words
        /// <summary>
        /// Query. By default terms are ANDed. To see what is possible, use our
        /// advanced search page to perform a search and then check the url for 
        /// the q value. (http://www.indeed.com/advanced_search)
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        private string build_tag_query(List<string> queries)
        {
            string tag = "&q=" + String.Join(" ", queries);

            return tag;
        }

        /// <summary>
        /// Takes a company name filter string, and turns it into a string that 
        /// will be aggregated into the LinkedIn API call. Will return posts that
        /// have an exact match in the Company Name field as the specified filter.
        /// </summary>
        /// <param name="filterQ"></param>
        /// <returns></returns>
        private string companyName_converter(string filterQ)
        {
            if (filterQ == null || filterQ.Equals("")) 
            {
                return "";
            }

            string tag = "&company-name=" + HttpUtility.UrlPathEncode(filterQ);

            return tag;
        }

        /// <summary>
        /// Takes a date filter string, and turns it into a string that will be
        /// aggregated into the LinkedIn API call. Will return posts that were
        /// created exactly on the date specified in the filter.
        /// </summary>
        /// <param name="filterQ"></param>
        /// <returns></returns>
        private string date_converter(string filterQ)
        {
            string date = "";

            return date;
        }

        #endregion
    }
}