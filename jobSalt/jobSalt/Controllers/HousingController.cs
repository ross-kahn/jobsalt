using jobSalt.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    [ReleaseOnlyAuthorization]
    public class HousingController : Controller
    {
        //
        // GET: /Housing/

        public ActionResult Index()
        {
            if (!ConfigLoader.SiteConfig.HousingEnabled)
            {
                throw new HttpException(404, "The page you requested could not be found");
            }

            return View();
        }

    }
}
