using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace jobSalt.Models
{
    public class IndeedQueryBuilder
    {
        // These should not change between users
        private const string FORMAT_TAG = "&format=json";
        private const string VERSION_TAG = "&v=2";
        
        // TODO: OBTAIN THESE FROM USER SESSION (hard-coded right now)
        private const string USER_IP = "1.2.3.4"; // 129.21.108.174
        private const string USER_AGENT = "Mozilla/%2F4.0%28Firefox%29";

        /// <summary>
        /// Not every API is able to filter by every Field specified by this program; some Fields
        /// need to be combined so relevant results are returned. This method combines relevant
        /// fields and eliminates the ones that simply cannot be translated into an API call
        /// </summary>
        /// <param name="filterHash">
        /// The original hash of filters
        /// </param>
        /// <returns>The modified, combined hash of filters</returns>
        private Dictionary<Field, List<string>> combineKeys(Dictionary<Field, List<string>> filterHash, Field[] toCombine)
        {
            if (toCombine == null)
            {
                return filterHash;
            }

            List<string> fos;
            if (filterHash.ContainsKey(Field.Keyword))
            {
                fos = filterHash[Field.Keyword];
            }
            else
            {
                fos = new List<string>();
            }
            
            foreach (Field keyField in toCombine){
                if (filterHash.ContainsKey(keyField))
                {
                    if (keyField == Field.CompanyName)
                    {
                        filterHash[keyField][0] = "company:(" + filterHash[keyField][0] + ")";
                    }
                    if (keyField == Field.JobTitle)
                    {
                        filterHash[keyField][0] = "title:(" + filterHash[keyField][0] + ")";
                    }
                    fos.AddRange(filterHash[keyField]);
                    filterHash.Remove(keyField);
                }
            }
            filterHash[Field.Keyword] = fos;
                        
            return filterHash;
        }

        public Field[] getCombineFields()
        {
            Field[] cFields = {   Field.CompanyName,
                                  Field.FieldOfStudy, 
                                  Field.JobTitle };
            return cFields;
        }

        public string buildQuery(Dictionary<Field, List<string>> FilterHash, int page, int resultsPerPage)
        {
            FilterHash = combineKeys(FilterHash, getCombineFields());

            // String builder, (arguably) more efficient than concatenating strings
            StringBuilder builder = new StringBuilder();

            // The required base for all requests
            builder.Append(Constants.INDEED_REQUEST_BASE);
            builder.Append("&start=" + page * resultsPerPage);
            builder.Append("&limit=" + resultsPerPage);

            foreach (Field key in FilterHash.Keys)
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
                        builder.Append(build_tag_query(FilterHash[Field.Keyword]));
                        break;

                    case Field.Location:
                        builder.Append("&l=" + FilterHash[key][0]);
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

        private string build_tag_location()
        {
            return "v=";
        }

        private string build_tag_sort( )
        {
            return "v=";
        }

        private string build_tag_radius( )
        {
            return "v=";
        }

        private string build_tag_sitetype( )
        {
            return "v=";
        }

        private string build_tag_jobtype( )
        {
            return "v=";
        }

        private string build_tag_start( )
        {
            return "v=";
        }

        private string build_tag_limit(string limit)
        {
            return "&limit=" + limit;
        }

        private string build_tag_fromage( )
        {
            return "v=";
        }

        private string build_tag_highlight( )
        {
            return "v=";
        }

        private string build_tag_filter( )
        {
            return "v=";
        }

        private string build_tag_latlong( )
        {
            return "v=";
        }

        private string build_tag_country( )
        {
            return "v=";
        }

        private string build_tag_chnl( )
        {
            return "v=";
        }

        /// <summary>
        /// The IP number of the end-user to whom the job results will be displayed. This field is required.
        /// </summary>
        /// <returns>
        /// Portion of an Indeed API call regarding user ip
        /// </returns>
        private string build_tag_userip(string ip)
        {
            return "&userip=" + ip;
        }

        /// <summary>
        /// The User-Agent (browser) of the end-user to whom the job results will be displayed. 
        /// This can be obtained from the "User-Agent" HTTP request header from the end-user. This field is required.
        /// </summary>
        /// <returns></returns>
        private string build_tag_useragent(string agent)
        {
            return "&useragent=" + agent;
        }

        #endregion
    }
}