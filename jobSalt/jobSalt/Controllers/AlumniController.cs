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
            ViewBag.FilterString = filterString;

            // If being called from ajax return the partial view that has the next set of alumni posts
            if (Request.IsAjaxRequest())
            {
                Dictionary<Models.Field, string> filters = jobSalt.Models.Filter.FilterQueryStringToDictionary(filterString);
                return PartialView("Index_Partial", shepard.GetAlumni(filters, page, resultsPerPage).ToArray());
            }

            return View();
        }

    }
}
