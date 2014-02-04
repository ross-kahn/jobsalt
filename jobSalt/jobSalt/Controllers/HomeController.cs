using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Models.Filter> farray = new List<Models.Filter>();
            farray.Add(new Models.Filter(Models.Field.Keyword, "Java"));

            Models.IndeedModule indeed = new Models.IndeedModule();
            List<Models.JobPost> temp = indeed.GetJobs(farray, 1);
            return View( temp);

            /**ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();**/
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
