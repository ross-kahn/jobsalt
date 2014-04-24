using jobSalt.Models;
using jobSalt.Models.Config;
using jobSalt.Models.Feature.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    [ReleaseOnlyAuthorization]
    public class SalaryController : Controller
    {
        private SalaryShepard shepard = new SalaryShepard();
        //
        // GET: /Salary/

        public ActionResult Index(string filterString)
        {
            if (!ConfigLoader.SiteConfig.SalaryEnabled)
            {
                throw new HttpException(404, "The page you requested could not be found");
            }

            FilterBag filters = FilterBag.createFromURLQuery(Request.QueryString.ToString());

            ViewBag.FilterString = filters.JsonEncode();
            ViewBag.FilterBag = filters;

            List<SalaryPost> salaries = shepard.GetSalaries(filters);
            
            return View(salaries);
        }

    }
}
