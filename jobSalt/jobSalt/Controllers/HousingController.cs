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
                return PartialView("Index_Partial", shepard.GetHousing(filters, page, resultsPerPage).ToArray());
            }
            return View(new HousingPost());
        }

        [HttpPost]
        public ActionResult AddReview(HousingPost post)
        {
            LocalHousingModule housingModule = new LocalHousingModule();
            string username = "";
            if (User != null && User.Identity != null)
                username = User.Identity.Name;
            housingModule.AddHousingPost(post, User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult RemoveReview(int postID)
        {
            LocalHousingModule housingModule = new LocalHousingModule();
            HousingPost post = housingModule.GetHousingPost(postID);

            if (User.IsInRole("admin") || User.Identity.Name.Equals(post.PostedBy))
            {
                housingModule.DeleteHousingPost(postID);
            }
            else
            {
                throw new HttpException(403, "Access Denied");
            }

            return null;
        }
    }
}
