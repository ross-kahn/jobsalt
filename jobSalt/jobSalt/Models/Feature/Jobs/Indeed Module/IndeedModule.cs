using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using jobSalt.Models.Data_Types;

namespace jobSalt.Models.Feature.Jobs.Indeed_Module
{
    public class IndeedModule : Feature.Jobs.IJobModule
    {

        private IndeedQueryBuilder builder; 

        public IndeedModule()
        {
            builder = new IndeedQueryBuilder();
        }

        public List<JobPost> GetJobs(FilterBag filterbag, int page, int resultsPerPage)
        {
            // TODO: Replace this with a Logger call
            string request = "";
            try
            {
                request = builder.buildQuery(filterbag, page, resultsPerPage);

                if (String.IsNullOrEmpty(request))
                {
                    throw new ArgumentException("Indeed API request is empty or null");
                }
            }
            catch (ArgumentException argex)
            {

            }
            catch (NullReferenceException nullex)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught while building Indeed Query: " + e.Message);
            }

            
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
                    Location = new Location(
                        raw.State,
                        raw.City,
                        null
                   ),
                    Salary = null,
                    Description = raw.Snippet,
                    FieldOfStudy = null
                };
                results.Add(jobpost);
            }
            return results;
        }

        Source source = new Source() { Name = "Indeed", Icon = @"\Content\images\indeed_icon.png" };
        public Source Source
        {
            get { return source; }
        }
    }
   
}