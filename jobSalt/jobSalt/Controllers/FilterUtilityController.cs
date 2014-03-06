using jobSalt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterUtilityController : Controller
    {
        public ActionResult AssignFilter(Field targetField, string value, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            fb.AssignFilter(new Models.Filter(targetField, value));
            string newFilterString = fb.JsonEncode();
            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + newFilterString);
        }

        public ActionResult RemoveFilter(Field targetField, string filterString)
        {
            string newFilterString = FilterUtility.RemoveFilter(targetField, filterString);
            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + newFilterString);
        }

        public JsonResult GetFilters(string filterString)
        {
            return Json(FilterUtility.GetFilters(filterString), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetFilterView(Field targetField, string filterString)
        {
            ViewBag.FilterString = filterString;
            ViewBag.inputID = new Guid().ToString();
            return PartialView("_FilterEditPartial", targetField);
        }
    }
}
