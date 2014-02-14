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
                Session["LoggedIntoLinkedIn"] = false;
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
                List<Models.Filter> filters = FilterUtility.GetFilters(filterString);
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

        private class linkedInResponse
        {
            public string expires_in;
            public string access_token;
        }

       
    }


}
