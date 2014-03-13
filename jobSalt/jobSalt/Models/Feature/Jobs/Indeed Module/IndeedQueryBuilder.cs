using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using jobSalt.Models.Data_Types;

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

        public string buildQuery(FilterBag filterbag, int page, int resultsPerPage)
        {

            // String builder, (arguably) more efficient than concatenating strings
            StringBuilder builder = new StringBuilder();

            // The required base for all requests
            builder.Append(Constants.INDEED_REQUEST_BASE);
            builder.Append("&start=" + page * resultsPerPage);

            builder.Append(keywordConverter(filterbag.Keyword, filterbag.JobTitle, filterbag.CompanyName));
            builder.Append(locationConverter(filterbag.Location));
        
            // Required tags
            builder.Append(FORMAT_TAG);                                 // The result comes back in JSON format
            builder.Append(limitConverter(Constants.RESULT_LIMIT));    // The limit of # of results returned
            builder.Append(useripConverter(USER_IP));                  
            builder.Append(useragentConverter(USER_AGENT));
            builder.Append(VERSION_TAG);

            return HttpUtility.UrlEncode(builder.ToString());
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

        private string keywordConverter (string keyword = "", string jobTitle = "", string companyName = "")
        {
            // Return an empty string if none of the paramters have values
            if (String.IsNullOrWhiteSpace(keyword + jobTitle + companyName))
            {
                return "";
            }

            string query = "&q=";

            if (!String.IsNullOrWhiteSpace(keyword))
            {
                query += keyword;
            }

            if (!String.IsNullOrWhiteSpace(jobTitle))
            {
                query += " title:" + jobTitle; 
            }

            if (!String.IsNullOrWhiteSpace(companyName))
            {
                query += " company:" + companyName;
            }

            return query;

        }

        private string locationConverter(Location loc)
        {
            string q = "&l=";

            if (String.IsNullOrWhiteSpace(loc.ZipCode))
            {
                q += loc.ZipCode;
            }
            else if (String.IsNullOrWhiteSpace(loc.City) && String.IsNullOrWhiteSpace(loc.State))
            {
                q += loc.City + ", " + loc.State;
            }
            else
            {
                return "";
            }

            return q;
        }

        private string limitConverter(string limit)
        {
            if (!String.IsNullOrWhiteSpace(limit))
            {
                return "&limit=" + limit;
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

        #endregion
    }
}