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

        public ActionResult Index(List<Models.Filter> filters = null, int page = 1, int resultsPerPage = 10)
        {
            // If filters is null replace it
            filters = filters ?? new List<Models.Filter>();

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

    }
}
