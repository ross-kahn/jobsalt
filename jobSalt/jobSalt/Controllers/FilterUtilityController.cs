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

            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + HttpUtility.UrlEncode(newFilterString));
        }

        public ActionResult RemoveFilter(Field targetField, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            fb.RemoveFilter(targetField);
            string newFilterString = fb.JsonEncode();

            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + HttpUtility.UrlEncode(newFilterString));
        }

        public PartialViewResult GetFilterView(Field targetField, string filterString)
        {
            ViewBag.FilterString = filterString;
            ViewBag.inputID = Guid.NewGuid();
            return PartialView("_FilterEditPartial", targetField);
        }
    }
}
