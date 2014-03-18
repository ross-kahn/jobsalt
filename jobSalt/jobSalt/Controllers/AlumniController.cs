using jobSalt.Models;
using jobSalt.Models.Feature.Alumni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class AlumniController : Controller
    {
        private AlumniShepard shepard = new AlumniShepard();
        //
        // GET: /Alumni/

        public ActionResult Index(string filterString, int page = 0, int resultsPerPage = 10)
        {
            FilterBag filters = FilterBag.createFromURLQuery(Request.QueryString.ToString());

            ViewBag.FilterString = filters.JsonEncode();
            ViewBag.FilterBag = filters;

            // If being called from ajax return the partial view that has the next set of alumni posts
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetAlumni(filters));
            }

            return View();
        }

        public ActionResult JobsAtCompany(string filterString, string company)
        {
            string newFilterString = FilterUtility.AssignFilter(Field.CompanyName, company, "");
            return RedirectToAction("Index", "Job", new { filterString = newFilterString });
        }

    }
}
