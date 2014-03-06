using jobSalt.Models;
using jobSalt.Models.Feature.Jobs;
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
        

        public ActionResult Index(string filterString, int page = 0, int resultsPerPage = 10)
        {
            if (Session["LoggedIntoLinkedIn"] == null)
            {
                Session["LoggedIntoLinkedIn"] = true;
            }

            ViewBag.FilterString = filterString;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                Dictionary<Models.Field, string> filters = jobSalt.Models.Filter.FilterQueryStringToDictionary(filterString);
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

        public ActionResult AlumniAtCompany(string filterString, string company)
        {
            string newFilterString = FilterUtility.AssignFilter(Field.CompanyName, company, "");
            return RedirectToAction("Index", "Alumni", new { filterString = newFilterString });
        }
    }


}
