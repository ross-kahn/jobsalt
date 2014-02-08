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

        public ActionResult Index(string query, string filterStrings, int page = 1, int resultsPerPage = 10)
        {
            List<Models.Filter> filters = Models.Filter.FilterQueryStringToList(filterStrings);
            if (!String.IsNullOrEmpty(query))
            {
                filters.Add(new Models.Filter(Models.Field.Keyword, query));
                filterStrings = Models.Filter.FilterListToUrlQueryString(filters);
            }

            ViewBag.Filters = filterStrings;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

    }
}
