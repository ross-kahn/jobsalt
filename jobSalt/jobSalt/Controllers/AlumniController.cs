using jobSalt.Models;
using jobSalt.Models.Config;
using jobSalt.Models.Feature.Alumni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    [ReleaseOnlyAuthorization]
    public class AlumniController : Controller
    {
        private AlumniShepard shepard = new AlumniShepard();
        //
        // GET: /Alumni/

        public ActionResult Index(string filterString, int page = 0)
        {
            if (!ConfigLoader.SiteConfig.HousingEnabled)
            {
                throw new HttpException(404, "The page you requested could not be found");
            }

            AlumniConfig config = ConfigLoader.AlumniConfig;
            int resultsPerPage = config.NumResults;
            
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

        [ChildActionOnly]
        public ActionResult JobsAtCompany(string filterString, string company)
        {
            string newFilterString = FilterUtility.AssignFilter(Field.CompanyName, company, "");
            return RedirectToAction("Index", "Job", new { filterString = newFilterString });
        }

    }
}
