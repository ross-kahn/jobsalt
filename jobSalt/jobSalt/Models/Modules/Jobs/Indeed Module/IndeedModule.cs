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

            string request = builder.buildQuery(filterHash, page, resultsPerPage);

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
                    URL = raw.URL,
                    SourceModule = source,
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

        Source source = new Source() { Name = "Indeed", Icon = null };
        public Source Source
        {
            get { return source; }
        }
    }
   
}