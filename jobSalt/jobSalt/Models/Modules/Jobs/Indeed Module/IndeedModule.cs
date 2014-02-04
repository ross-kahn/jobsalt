using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;

namespace jobSalt.Models
{
    public class IndeedModule : Modules.Jobs.IJobModule
    {

        private IndeedQueryBuilder builder; 

        public IndeedModule()
        {
            builder = new IndeedQueryBuilder();
        }

        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsPerPage)
        {
            Dictionary<Field, List<string>> filterHash = Filter.FilterListToDictionary(filters);

            //string request = buildRequest(filterHash);

            string request = builder.buildQuery(filterHash);

            IndeedResult iResult;
            using (var client = new WebClient())
            {
                string json = client.DownloadString(request);
                var serializer = new JavaScriptSerializer();
                // TODO: Fetch the JSON from a remote URL
                iResult = serializer.Deserialize<IndeedResult>(json);
            }

            return IndeedResultToJobPosts(iResult);
        }

        private List<JobPost> IndeedResultToJobPosts(IndeedResult iResult)
        {
            List<JobPost> results = new List<JobPost>();
            foreach( IndeedJobPost raw in iResult.Results){
                JobPost jobpost = new JobPost(){
                    //Source = null,
                    DatePosted = raw.Date,
                    Company = raw.Company,
                    JobTitle = raw.JobTitle,
                    //Location = null,
                    Salary = null,
                    Description = raw.Snippet,
                    FieldOfStudy = null
                };
                results.Add(jobpost);
            }
            return results;
        }

        /** The order that the request is built in matters; tags that are empty
         * also need to be included in the request. Because of those two things,
         * this method goes one-by-one through each required tag and attempts to
         * build it using the filters provided. If no filter is provided for a 
         * particular tag, it uses its default
         **/
        private string buildRequest(Dictionary<Field, List<string>> FilterHash)
        {
            return builder.buildQuery(FilterHash);
        }


        // TODO: ONLY PASS IN THE LIST OF STRINGS, NOT THE ENTIRE DICTIONARY -Ross

        public int ResultsPerPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DisplayName
        {
            get
            {
                return Constants.INDEED_DISPLAY_NAME;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        
    }
   
}