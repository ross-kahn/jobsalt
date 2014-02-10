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
        public ActionResult AssignFilter(Field targetField, string value)
        {
            FilterUtility.AssignFilter(targetField, value);
            return null;
        }

        public ActionResult RemoveFilter(Field targetField)
        {
            FilterUtility.RemoveFilter(targetField);
            return null;
        }

        public JsonResult GetFilters()
        {
            return Json(FilterUtility.GetFilters());
        }
    }
}
