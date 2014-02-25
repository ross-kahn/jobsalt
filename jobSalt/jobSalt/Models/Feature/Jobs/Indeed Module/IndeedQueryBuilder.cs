using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace jobSalt.Models.Feature.Jobs.Indeed_Module
{
    public class IndeedQueryBuilder
    {
        // These should not change between users
        private const string FORMAT_TAG = "&format=json";
        private const string VERSION_TAG = "&v=2";
        
        // TODO: OBTAIN THESE FROM USER SESSION (hard-coded right now)
        private const string USER_IP = "1.2.3.4"; // 129.21.108.174
        private const string USER_AGENT = "Mozilla/%2F4.0%28Firefox%29";

        private Dictionary<Field, string> combineFields(Dictionary<Field, string> filterHash)
        {
            // Check for trailing / leading spaces
            if (!filterHash.ContainsKey(Field.Keyword))
            {
                filterHash[Field.Keyword] = "";
            }

            if (filterHash.ContainsKey(Field.FieldOfStudy))
            {
                filterHash[Field.Keyword] += " " + filterHash[Field.FieldOfStudy];
                filterHash.Remove(Field.FieldOfStudy);
            }

            if (filterHash.ContainsKey(Field.CompanyName))
            {
                filterHash[Field.Keyword] += " company:" + filterHash[Field.CompanyName];
                filterHash.Remove(Field.CompanyName);
            }

            if (filterHash.ContainsKey(Field.JobTitle))
            {
                filterHash[Field.Keyword] += " title:" + filterHash[Field.JobTitle];
                filterHash.Remove(Field.JobTitle);
            }
            return filterHash;
        }

        public string buildQuery(Dictionary<Field, string> filterHash, int page, int resultsPerPage)
        {

            filterHash = combineFields(filterHash);

            // String builder, (arguably) more efficient than concatenating strings
            StringBuilder builder = new StringBuilder();

            // The required base for all requests
            builder.Append(Constants.INDEED_REQUEST_BASE);
            builder.Append("&start=" + page * resultsPerPage);

            foreach (Field key in filterHash.Keys)
            {
                switch (key)
                {
                    case Field.CompanyName:  // Should be combined with keyword
                        break;

                    case Field.Date:
                        break;

                    case Field.FieldOfStudy: // Should be combined with keyword
                        break;

                    case Field.JobTitle:    // Should be combined with keyword
                        break;

                    case Field.Keyword:
                        builder.Append(keywordConverter(filterHash[key]));
                        break;

                    case Field.Location:
                        builder.Append("&l=" + filterHash[key]);
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
            builder.Append(limitConverter(Constants.RESULT_LIMIT));    // The limit of # of results returned
            builder.Append(useripConverter(USER_IP));                  
            builder.Append(useragentConverter(USER_AGENT));
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

        private string keywordConverter (string queries)
        {
            if (isValidFilterQ(queries))
            {
                string tag = String.Join("+", queries.Split(' '));

                return "&q=" + tag;
            }
            else
            {
                return "";
            }
        }

        private string limitConverter(string limit)
        {
            if (isValidFilterQ(limit))
            {
                return "&limit=" + limit;
            }
            else
            {
                return "";
            }
        }

        private string jobtitleConverter(string query)
        {
            if (isValidFilterQ(query))
            {
                string result = "&as_ttl=" + String.Join("+", query);

                return result;
            }
            else
            {
                return "";
            }
        }

        private string companyNameConverter(string query)
        {
            if (isValidFilterQ(query))
            {
                string result = "&as_cmp=" + String.Join("+", query);
                return result;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// The IP number of the end-user to whom the job results will be displayed. This field is required.
        /// </summary>
        /// <returns>
        /// Portion of an Indeed API call regarding user ip
        /// </returns>
        private string useripConverter(string ip)
        {
            return "&userip=" + ip;
        }

        /// <summary>
        /// The User-Agent (browser) of the end-user to whom the job results will be displayed. 
        /// This can be obtained from the "User-Agent" HTTP request header from the end-user. This field is required.
        /// </summary>
        /// <returns></returns>
        private string useragentConverter(string agent)
        {
            return "&useragent=" + agent;
        }

        private static bool isValidFilterQ(string filterQ)
        {
            // What other characters do we need to catch?
            if (filterQ == null || filterQ.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}