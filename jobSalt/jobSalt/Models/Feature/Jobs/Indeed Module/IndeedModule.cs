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
            if (  filterbag.isEmpty()  ) {  return new List<JobPost>();   }


            // Will try and build an Indeed API request from the given set of filters. Catches and logs any problems
            string request = "";
            try
            {
                // Build the request based on filters
                request = builder.buildQuery(filterbag, page, resultsPerPage);

                // If the request comes back empty, something bad happened
                if (String.IsNullOrEmpty(request)) { throw new ArgumentException(); }
            
            
            }


            // The built request is, for some reason, empty or null. Return empty results list
            catch (ArgumentException argex) 
            {
                Logging.JobSaltLogger.Instance.log("(Indeed) Error in IndeedQueryBuilder caused API request string to be empty or null.");
                Logging.JobSaltLogger.Instance.log(filterbag.ToString() + "\n Page=" + page + "\n resultsPerPage=" + resultsPerPage);
                return new List<JobPost>();
            }


            // An unknown exception was thrown when trying to build the request. The request string
            // is empty now, so there's no point trying to continue (since it will return an empty list anyway)
            catch (Exception e)         
            {
                Logging.JobSaltLogger.Instance.log("(Indeed) Exception caught while building Indeed Query: " + e.Message);
                Logging.JobSaltLogger.Instance.log(filterbag.ToString() + "\n Page=" + page + "\n resultsPerPage=" + resultsPerPage);
                return new List<JobPost>();
            }

            
            IndeedResult iResult;   // Raw Indeed results
            using (var client = new WebClient())
            {
                string json = client.DownloadString(request);   // Issues a Get to the Indeed API with the request string

                try
                {

                    var serializer = new JavaScriptSerializer();
                    iResult = serializer.Deserialize<IndeedResult>(json);   // Parses JSON result into C# Indeed data object

                    // The raw Indeed results are null... something very bad happened!
                    if (null == iResult) { throw new ArgumentException();}

                    // This is used to make sure that if a higher page is requested than Indeed has,
                    // then the last several posts don't keep getting returned (fixed bug)
                    int startpost = resultsPerPage * page;

                    return IndeedResultToJobPosts(iResult, startpost); // Parses C# Indeed data object into a list of JobPosts

                }

                // iResult is null, which means something went very wrong. Return an empty job list
                catch (ArgumentException argex)    
                {
                    Logging.JobSaltLogger.Instance.log("(Indeed) An error occured when parsing Indeed JSON into iResult, resulting in iResult being null: " + argex.Message);
                    Logging.JobSaltLogger.Instance.log("(Indeed) JSON: \n" + json);
                    return new List<JobPost>();
                }

                // An unknown exception occured
                catch (Exception e)     
                {
                    Logging.JobSaltLogger.Instance.log("(Indeed) An exception occured when parsing Indeed JSON into iResult: " + e.Message);
                    Logging.JobSaltLogger.Instance.log("(Indeed) JSON: \n" + json);
                    return new List<JobPost>();
                }
            }
        }


        /// <summary>
        /// Takes an object consisting of raw, Indeed-specific results and parses into a
        /// jobSalt-standardized data object. If a higher page is requested than Indeed has results,
        /// (e.g. Indeed has 1000 results and a page starting at result 1001 is requested), then
        /// this method will return an empty result list.
        /// </summary>
        /// <param name="iResult"></param>
        /// <param name="startpost"></param>
        /// <returns></returns>
        private List<JobPost> IndeedResultToJobPosts(IndeedResult iResult, int startpost)
        {
            List<JobPost> results = new List<JobPost>();

            // If we try and get results even after Indeed has shown all it can,
            // Indeed will just return the last 10 or so. To avoid that, short circuit
            // once we've run out of possible posts
            if (startpost >= iResult.TotalResults)
            {
                return results;
            }

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