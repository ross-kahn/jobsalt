using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using jobSalt.Models.Modules.Jobs.LinkedIn_Module;


namespace jobSalt.Models
{
    public class LinkedInModule : Modules.Jobs.IJobModule
    {
        private const string rossaccess = "AQQonrngGyxXjYy1oGzW0MnyrTZZ-n7OFVGW1WeBfsZiU6HX02BSFPF5pw-M0iSM-vUjbR7335D7y-P_XvpCHKHJQ_23gLF52_n_NWJPhuQ4kmXq6TY";


        string auth = "https://api.linkedin.com/v1/job-search?keywords=java&sort=R&oauth2_access_token=AQUNc2o0RW5uz6KbNibyy45vdhkbD-OSAbeuG5KGa7GphcwxdF-9Ix4Pf0Ot1N5sB57LLhavAZGCBqlBE6Mdy1LjWbUiUDXht-8dUaFQjWUo9SgascThj5E4-BZ3L8IeGMobOhAeh6mU4j_eWYbI9maLJeqS7WFrA7jVmu0IOSgLSoW-Dl4";
        string request = "https://api.linkedin.com/v1/job-search?keywords=java&sort=R";
        string authurl = "https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code" +
                                           "&code=" + rossaccess +
                                           "&redirect_uri=http://localhost:38087/" +
                                           "&client_id=75wt0uzfa9hfro" +
                                           "&client_secret=d1a6ef5f-210d-4589-8f94-a83c0aee1708";

        
        public LinkedInModule()
        {

        }

        public List<JobPost> GetJobs(Dictionary<Field, string> filters, int page, int resultsPerPage)
        {
            /*if (filters.Count == 0)
            {
                return new List<JobPost>();
            }

            using (var client = new WebClient())
            {
                string xml = client.DownloadString(new Uri(auth));
                Console.WriteLine(xml);
            }

            return new List<JobPost>();*/
            LinkedInTempData tempData = new LinkedInTempData();
            LinkedInResult results = tempData.getDummyData();

            return LinkedInResultToJobPosts(results);
        }

        private List<JobPost> LinkedInResultToJobPosts(LinkedInResult linkedinResult)
        {
            List<JobPost> results = new List<JobPost>();

            foreach (LinkedInJobPost raw in linkedinResult.jobs.values)
            {
                var loc = raw.locationDescription.Split(',');
                JobPost jobpost = new JobPost()
                {
                    Company = raw.company.name,
                    Description = raw.descriptionSnippet,
                    Location = new Location(null, loc[1], loc[0]),
                    URL = raw.siteJobUrl,
                    SourceModule = source,
                    DatePosted = new DateTime(raw.postingDate.year, 
                                                raw.postingDate.month, 
                                                raw.postingDate.day),
                    JobTitle = raw.position.title,
                    Salary = null
                };
                results.Add(jobpost);
            }

            return results;
        }

        Source source = new Source() { Name = "LinkedIn", Icon = @"\Content\images\linkedin_icon.jpg" };
        public Source Source
        {
            get { return source; }
        }
    }
}