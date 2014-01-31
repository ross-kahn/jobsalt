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
            Models.Filter[] farray = {new Models.Filter(Models.Field.Keyword, "Java")};

            Models.Indeed_Module indeed = new Models.Indeed_Module();
            Models.IndeedJobPost[] temp = indeed.GetResults(farray).Results;
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
