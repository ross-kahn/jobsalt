using jobSalt.Models;
using jobSalt.Models.Modules.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace jobSalt.Controllers
{
    public class JobController : Controller
    {
        private JobShepard shepard = new JobShepard();
        

        public ActionResult Index(string filterString, int page = 1, int resultsPerPage = 10)
        {
            if (Session["LoggedIntoLinkedIn"] == null)
            {
                Session["LoggedIntoLinkedIn"] = true;
            }

            if ((bool)Session["LoggedIntoLinkedIn"] == false)
            {
                return Redirect("https://www.linkedin.com/uas/oauth2/authorization?response_type=code" +
                                           "&client_id=75tu3x63buelpy" +
                                           "&state=DCEEFWF45453sdffef424jobsaltisthebesthingever32472134721324r" +
                                           "&redirect_uri=http://localhost:38087/Job/LinkedInLogin");
            }

            ViewBag.FilterString = filterString;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                //List<Models.Filter> filters = FilterUtility.GetFilters(filterString);
                Dictionary<Models.Field, string> filters = jobSalt.Models.Filter.FilterQueryStringToDictionary(filterString);
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

        private class linkedInResponse
        {
            public string expires_in;
            public string access_token;
        }

        public ActionResult LinkedInLogin(string code, string state)
        {
            Session["LoggedIntoLinkedIn"] = true;

            using (var client = new WebClient())
            {
                string request = "https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code" +
                                           "&code=" + code +
                                           "&redirect_uri=http://localhost:38087/Job/LinkedInLogin" +
                                           "&client_id=75tu3x63buelpy" +
                                           "&client_secret=hBVkeuOMTd1sCGRe";

                string json = client.DownloadString(request);

                var serializer = new JavaScriptSerializer();
                linkedInResponse response = serializer.Deserialize<linkedInResponse>(json);


                string jobRequest = "https://api.linkedin.com/v1/job-search?keywords=java&sort=DA&oauth2_access_token=" + response.access_token;
                string jobs = client.DownloadString(jobRequest);
            }

            return RedirectToAction("Index");
        }
    }


}
