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
            // Short circuit if there are no filters specified
            if (filterbag.isEmpty())
            {
                return new List<JobPost>();
            }

            // Will try and build an Indeed API request from the given set of filters. Catches and logs any problems
            string request = "";
            try
            {
                request = builder.buildQuery(filterbag, page, resultsPerPage);

                if (String.IsNullOrEmpty(request)) { throw new ArgumentException(); }
            }
            catch (ArgumentException argex) // The built request is, for some reason, empty or null. Return empty results list
            {
                Logging.JobSaltLogger.Instance.log("(Indeed) Error in IndeedQueryBuilder caused API request string to be empty or null.");
                Logging.JobSaltLogger.Instance.log(filterbag.ToString() + "\n Page=" + page + "\n resultsPerPage=" + resultsPerPage);
                return new List<JobPost>();
            }
            catch (Exception e)         // An unknown exception was thrown; may not be fatal, so log it and continue
            {
                Logging.JobSaltLogger.Instance.log("(Indeed) Exception caught while building Indeed Query: " + e.Message);
                Logging.JobSaltLogger.Instance.log(filterbag.ToString() + "\n Page=" + page + "\n resultsPerPage=" + resultsPerPage);
            }

            
            IndeedResult iResult;   // Raw Indeed results
            using (var client = new WebClient())
            {
                string json = client.DownloadString(request);   // Issues a Get to the Indeed API with the request string

                try
                {
                    var serializer = new JavaScriptSerializer();
                    iResult = serializer.Deserialize<IndeedResult>(json);   // Parses JSON result into C# Indeed data object

                    if (null == iResult) { throw new ArgumentException();}

                    return IndeedResultToJobPosts(iResult); // Parses C# Indeed data object into a list of JobPosts
                }
                catch (ArgumentException argex)    // iResult is null, which means something went very wrong. Return an empty job list
                {
                    Logging.JobSaltLogger.Instance.log("(Indeed) An error occured when parsing Indeed JSON into iResult, resulting in iResult being null: " + argex.Message);
                    Logging.JobSaltLogger.Instance.log("(Indeed) JSON: \n" + json);
                    return new List<JobPost>();
                }
                catch (Exception e)     // An unknown exception occured
                {
                    Logging.JobSaltLogger.Instance.log("(Indeed) An exception occured when parsing Indeed JSON into iResult: " + e.Message);
                    Logging.JobSaltLogger.Instance.log("(Indeed) JSON: \n" + json);
                    return new List<JobPost>();
                }
            }
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