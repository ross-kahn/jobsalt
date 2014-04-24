using jobSalt.Models.Config;
using jobSalt.Models;
using jobSalt.Models.Feature.Housing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jobSalt.Models.Feature.Housing.LocalModule;

namespace jobSalt.Controllers
{
    [ReleaseOnlyAuthorization]
    public class HousingController : Controller
    {
        HousingShepard shepard = new HousingShepard();

        public ActionResult Index(string filterString, int page = 0, int resultsPerPage = 10)
        {
            if (!ConfigLoader.SiteConfig.HousingEnabled)
            {
                throw new HttpException(404, "The page you requested could not be found");

            }

            FilterBag filters = FilterBag.createFromURLQuery(Request.QueryString.ToString());

            ViewBag.FilterString = filters.JsonEncode();
            ViewBag.FilterBag = filters;

            // If being called from ajax return the partial view that has the next set of job posts
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index_Partial", shepard.GetHousing(filters).ToArray());
            }
            return View(new HousingPost());
        }

        [HttpPost]
        public ActionResult AddReview(HousingPost post)
        {
            LocalHousingModule housingModule = new LocalHousingModule();
            //housingModule.AddHousingPost(post);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ReleaseOnlyAuthorization(Roles="Admin")]
        public ActionResult RemoveReview(int postID)
        {
            LocalHousingModule housingModule = new LocalHousingModule();
            housingModule.DeleteHousingPost(postID);

            return RedirectToAction("Index");
        }
    }
}
