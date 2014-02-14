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
        private const string rossaccess = "AQQonrngGyxXjYy1oGzW0MnyrTZZ-n7OFVGW1WeBfsZiU6HX02BSFPF5pw-M0iSM-vUjbR7335D7y-P_XvpCHKHJQ_23gLF52_n_NWJPhuQ4kmXq6TY";


        string auth = "GET /v1/job-search?keywords=java&sort=R&oauth2_access_token=AQVPCZG9zxHvDUB3itP-tnz7ZUyCe8SGwltTVxqlA_v7kBkuFQeGbuokjl12lDlLAdFrEqoUmyvFl3jQl1FYn-vB06McMdyDxCiOQezIdq2fiy9U6myRxUJnmb2V79kCsYcA8_nL6pbgMzvGPKaZKykcQmjIRSP5r_D5SZBOKF_Q4HQ4KU4 HTTP/1.1";
        string request = "https://api.linkedin.com/v1/job-search?keywords=java&sort=R";
        string authurl = "https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code" +
                                           "&code=" + rossaccess +
                                           "&redirect_uri=http://localhost:38087/" +
                                           "&client_id=75wt0uzfa9hfro" +
                                           "&client_secret=d1a6ef5f-210d-4589-8f94-a83c0aee1708";

        
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
                string xml = client.DownloadString(authurl);
                //string xml = client.UploadString(authurl);
                Console.WriteLine(xml);
            }

            return new List<JobPost>();
        }
    }
}