using jobSalt.Models.Modules.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class JobController : Controller
    {
        private JobShepard shepard = new JobShepard();

        public ActionResult Index()
        {
            return View(shepard.GetJobs(new List<Models.Filter>(), 1, 10).ToArray());
        }

    }
}
