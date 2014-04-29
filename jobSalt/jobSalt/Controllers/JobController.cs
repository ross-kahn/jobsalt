using jobSalt.Models;
using jobSalt.Models.Config;
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
    [ReleaseOnlyAuthorization]
    public class JobController : Controller
    {
        private JobShepard shepard = new JobShepard();
        
        /// <summary>
        /// Generates the View to show
        /// </summary>
        /// <param name="filterString">set of filters to search on, taken from URL</param>
        /// <param name="page">page of results to grab</param>
        /// <returns>page</returns>
        public ActionResult Index(string filterString, int page = 0)
        {
            JobConfig config = ConfigLoader.JobConfig;
            int resultsPerPage = config.NumResults;

            //Convert filter URL into FilterBag
            FilterBag filters = FilterBag.createFromURLQuery(Request.QueryString.ToString());

            ViewBag.FilterString = filters.JsonEncode();
            ViewBag.FilterBag = filters;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetJobs(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

        /// <summary>
        /// Button Controller for Search Alumni button on each job
        /// </summary>
        /// <param name="filterString">filter for alumni page after switching</param>
        /// <param name="company">company to search alumni for</param>
        /// <returns></returns>
        public ActionResult AlumniAtCompany(string filterString, string company)
        {
            string newFilterString = FilterUtility.AssignFilter(Field.CompanyName, company, "");
            return RedirectToAction("Index", "Alumni", new { filterString = newFilterString });
        }
    }


}
