using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;


namespace jobSalt.Models
{
    public class LinkedInModule : Modules.Jobs.IJobModule
    {

        string auth = "https://api.linkedin.com/v1/job-search?keywords=java&sort=R&oauth2_access_token=AQUNc2o0RW5uz6KbNibyy45vdhkbD-OSAbeuG5KGa7GphcwxdF-9Ix4Pf0Ot1N5sB57LLhavAZGCBqlBE6Mdy1LjWbUiUDXht-8dUaFQjWUo9SgascThj5E4-BZ3L8IeGMobOhAeh6mU4j_eWYbI9maLJeqS7WFrA7jVmu0IOSgLSoW-Dl4";
        string request = "https://api.linkedin.com/v1/job-search?keywords=java&sort=R";
        string authurl = "https://www.linkedin.com/uas/oauth2/authorization?response_type=code"+
                                           "&client_id=75wt0uzfa9hfro"+
                                           "&state=DCEEFWF45453sdffef424jobsaltisthebesthingever32472134721324r"+
                                           "&redirect_uri=http://localhost:38087/";

        public LinkedInModule()
        {

        }


        public Source Source
        {
            get { throw new NotImplementedException(); }
        }

        public List<JobPost> GetJobs(List<Filter> filters, int page, int resultsPerPage)
        {
            /*if (filters.Count == 0)
            {
                return new List<JobPost>();
            }*/

            using (var client = new WebClient())
            {
                string xml = client.DownloadString(new Uri(auth));
                Console.WriteLine(xml);
            }

            return new List<JobPost>();
        }
    }
}