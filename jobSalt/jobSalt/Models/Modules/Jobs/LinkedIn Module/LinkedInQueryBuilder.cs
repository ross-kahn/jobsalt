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

        /// <summary>
        /// Combines Field of Study search words with Keywords. The Field of Study
        /// is surrounded in quotation marks to denote a requirement
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        private Dictionary<Field, string> combineKeys(Dictionary<Field, string> filters)
        {
            if (filters.ContainsKey(Field.FieldOfStudy))
            {
                string newstring = "\"" + filters[Field.FieldOfStudy] + "\"";
                string keywords = filters[Field.Keyword] ?? "";
                filters[Field.Keyword] = newstring + keywords; 
                filters.Remove(Field.FieldOfStudy);
            }

            return filters;
        }

        public string buildQuery(Dictionary<Field, string> filterHash, int page, int resultsPerPage)
        {
            if (filterHash.Keys.Count == 0) // Is this the best way of determining an error state?
            {
                return "";  // what should we return here? What error should we log?
            }

            filterHash = combineKeys(filterHash);

            // String builder, (arguably) more efficient than concatenating strings
            StringBuilder builder = new StringBuilder();

            // The required base for all requests
            builder.Append(FORMAT_TAG);                                 // The result comes back in JSON format
            builder.Append(Constants.LINKEDIN_REQUEST_BASE);
            builder.Append("&start=" + (page * resultsPerPage).ToString());
            builder.Append("&count=" + resultsPerPage.ToString());
            builder.Append("&sort=DD");

            foreach (Field key in filterHash.Keys)
            {
                switch (key)
                {
                    case Field.CompanyName:  
                        builder.Append(companyName_converter(filterHash[Field.CompanyName]));
                        break;

                    case Field.Date:
                        // THIS DOES NOT WORK!!
                        // TODO: Make sure the value in the filterhash is the proper format
                        builder.Append(date_converter(filterHash[Field.Date]));
                        break;

                    case Field.FieldOfStudy:
                        // For now, this is being treated as a keyword. Test/optimize this
                        break;

                    case Field.JobTitle:
                        builder.Append(jobTitle_converter(filterHash[Field.JobTitle]));
                        break;

                    case Field.Keyword:
                        builder.Append(keyword_converter(filterHash[Field.Keyword]));
                        break;

                    case Field.Location:
                        // For now, assumes that the location is a postal code
                        builder.Append(location_converter(filterHash[Field.Location]));
                        break;

                    case Field.Salary:
                        // Deprecated... not useful enough to enhance user experience!
                        break;

                    default:
                        break;
                }
            }

            return builder.ToString();
        }

        #region Tag String Builders

        private string build_tag_format(List<string> filterResults)
        {
            return "&format=json";
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
            if( isValidFilterQ(filterQ)){
                string tag = "&company-name=" + HttpUtility.UrlPathEncode(filterQ);

                return tag;
            }
            else
            {
                return "";
            }
        }

        
        /// <summary>
        /// Takes a date filter string, and turns it into a string that will be
        /// aggregated into the LinkedIn API call. Will return posts that were
        /// created exactly on the date specified in the filter.
        /// 
        /// WARNING: Do NOT use this method! No guarantees have been made about
        /// the format of the date, and LinkedIn's API filter for this value is
        /// broken anyway.
        /// </summary>
        /// <param name="filterQ"></param>
        /// <returns></returns>
        private string date_converter(string filterQ)
        {
            if (isValidFilterQ(filterQ) && Filter.isValidDateURL(filterQ))
            {
                string date = "&facet=date-posted," + filterQ;

                return date;
            }
            else
            {
                return "";
            }
        }

        private string jobTitle_converter(string filterQ)
        {
            if (isValidFilterQ(filterQ))
            {
                string jt = "&job-title=" + HttpUtility.UrlPathEncode(filterQ);

                return jt;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// For now, assumes that the input is a postal code
        /// </summary>
        /// <param name="filterQ"></param>
        /// <returns></returns>
        private string location_converter(string filterQ)
        {
            if (isValidFilterQ(filterQ))
            {
                string loc = "&country-code=us&postal-code=" + filterQ;
                return loc;
            }
            else
            {
                return "";
            }
        }

        private string keyword_converter(string filterQ)
        {
            if (isValidFilterQ(filterQ))
            {
                string keyword = "&keyword=" + HttpUtility.UrlPathEncode(filterQ);

                return keyword;
            }
            else
            {
                return "";
            }
        }

        private bool isValidFilterQ(string filterQ)
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