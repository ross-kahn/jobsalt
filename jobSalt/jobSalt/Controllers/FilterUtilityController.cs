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
            string newFilterString = FilterUtility.AssignFilter(targetField, value, filterString);
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
            return PartialView("_FilterEditPartial", targetField);
        }
    }
}
