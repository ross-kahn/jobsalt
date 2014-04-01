using jobSalt.Models;
using jobSalt.Models.Feature.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class SalaryController : Controller
    {
        private SalaryShepard shepard = new SalaryShepard();
        //
        // GET: /Salary/

        public ActionResult Index(string filterString)
        {
            FilterBag filters = FilterBag.createFromURLQuery(Request.QueryString.ToString());

            ViewBag.FilterString = filters.JsonEncode();
            ViewBag.FilterBag = filters;

            List<int> data = new List<int>();

            List<SalaryPost> salaries = shepard.GetAlumni(filters);
            foreach(SalaryPost salaryPost in salaries)
            {
                data.Add(salaryPost.Min);
                data.Add(salaryPost.Median);
                data.Add(salaryPost.Max);
            }
            
            return View((object)System.Web.Helpers.Json.Encode(data));
        }

    }
}
