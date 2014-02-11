using jobSalt.Models;
using jobSalt.Models.Modules.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class JobController : Controller
    {
        private JobShepard shepard = new JobShepard();

        public ActionResult Index(string filterString, int page = 1, int resultsPerPage = 10)
        {
            ViewBag.FilterString = filterString;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                List<Models.Filter> filters = FilterUtility.GetFilters(filterString);
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }
    }
}
