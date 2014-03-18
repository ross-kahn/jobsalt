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
    
        [HttpPost]
        public ActionResult AssignFilter(Field targetField, string value, string filterString)
        {
            FilterBag fb = FilterBag.createFromJSON(filterString);
            fb.AssignFilter(new Models.Filter(targetField, value));
            string newFilterString = fb.JsonEncode();

            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + newFilterString);
        }

        [HttpGet]
        public ActionResult RemoveFilter(Field targetField, string filterString)
        {
            FilterBag fb = FilterBag.createFromURLQuery(Request.QueryString.ToString());
            fb.RemoveFilter(targetField);
            string newFilterString = fb.JsonEncode();

            return Redirect(Request.UrlReferrer.AbsolutePath.ToString() + "?filterString=" + newFilterString);
        }

        [HttpGet]
        public PartialViewResult GetFilterView(Field targetField, string filterString)
        {
            FilterBag fb = FilterBag.createFromURLQuery(Request.QueryString.ToString());
            ViewBag.FilterString = fb.JsonEncode();
            ViewBag.inputID = Guid.NewGuid();
            return PartialView("_FilterEditPartial", targetField);
        }
    }
}
