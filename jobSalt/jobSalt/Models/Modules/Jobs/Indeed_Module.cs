using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace jobSalt.Models
{
    public class Indeed_Module
    {

        public IndeedResult GetResults(Filter[] filters)
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

            string request = buildRequest(FilterHash);

            using (var client = new WebClient())
            {
                string json = client.DownloadString(request);
                var serializer = new JavaScriptSerializer();
                // TODO: Fetch the JSON from a remote URL
                return serializer.Deserialize<IndeedResult>(json);
            }
        }

        /** The order that the request is built in matters; tags that are empty
         * also need to be included in the request. Because of those two things,
         * this method goes one-by-one through each required tag and attempts to
         * build it using the filters provided. If no filter is provided for a 
         * particular tag, it uses its default
         **/
        private string buildRequest(Dictionary<Field, List<string>> FilterHash)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Constants.INDEED_REQUEST_BASE);

            builder.Append(build_tag_query(FilterHash));
            /**foreach (Field key in FilterHash.Keys){
                switch (key)
                {
                        
                    case Field.Company_Name:
                        break;
                    case Field.Job_Title:
                        break;
                    case Field.Keyword:
                        break;
                }
            }**/

            // As of this release, these tags must be constant; that's why
            // 'null' is passed in. If, in the future, it is desireable to
            // have these tags be dependant on a certain Filter, the hash
            // can be passed in
            builder.Append(build_tag_format(null));
            builder.Append(build_tag_limit(null));      // This is defined in the config
            builder.Append(build_tag_userip(null));
            builder.Append(build_tag_useragent(null));
            builder.Append(build_tag_version(null));

            string request = builder.ToString();
            Console.WriteLine("Indeed_Module.cs ~90: " + request);
            return request;
        }

        // TODO: ONLY PASS IN THE LIST OF STRINGS, NOT THE ENTIRE DICTIONARY -Ross

        #region Tag String Builders

        private string build_tag_version(Dictionary<Field, List<string>> FilterHash)
        {
            return "&v=2";
        }

        private string build_tag_format(List<string> filterResults)
        {
            return "&format=json";
        }

        // Indeed supports advanced search, which (depending on how you wanted the search criteria)
        // would change how this string is formated for the request. For now, it's formatted in
        // the default way, which is putting a logical "AND" on each of the search words
        private string build_tag_query(Dictionary<Field, List<string>> FilterHash)
        {
            string tag = "&q=";

            // Joins the query strings together into an 'AND' format for the request
            if (FilterHash.ContainsKey(Field.Keyword))
            {
                tag = tag + String.Join("+", FilterHash[Field.Keyword]);
            }
            
            return tag;
        }

        private string build_tag_location(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_sort(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_radius(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_sitetype(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_jobtype(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_start(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_limit(Dictionary<Field, List<string>> FilterHash)
        {
            return "&limit="+ Constants.RESULT_LIMIT;
        }

        private string build_tag_fromage(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_highlight(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_filter(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_latlong(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_country(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_chnl(Dictionary<Field, List<string>> FilterHash)
        {
            return "v=";
        }

        private string build_tag_userip(Dictionary<Field, List<string>> FilterHash)
        {
            return "&userip=1.2.3.4";
        }

        private string build_tag_useragent(Dictionary<Field, List<string>> FilterHash)
        {
            return "&useragent=Mozilla/%2F4.0%28Firefox%29";
        }

#endregion
    }

    public class IndeedResult
    {
        public int Version { get; set; }
        public string Query { get; set; }
        public string Location { get; set; }
        public bool DupeFilter { get; set; }
        public bool Highlight { get; set; }
        public int Radius { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int TotalResults { get; set; }
        public int PageNumber { get; set; }
        public IndeedJobPost[] Results { get; set; }
    }

    public class IndeedJobPost
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FormattedLocation { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public string Snippet { get; set; }
        public string URL { get; set; }
        public string OnMouseDown { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string JobKey { get; set; }
        public bool Sponsored { get; set; }
        public bool Expired { get; set; }
        public string FormattedLocationFull { get; set; }
        public string FormattedRelativeTime { get; set; }
    }
}