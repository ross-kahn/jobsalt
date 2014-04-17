using jobSalt.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobSalt.Controllers
{
    public class CssViewResult : PartialViewResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/css";
            base.ExecuteResult(context);
        }
    }

    [ReleaseOnlyAuthorization(Roles = "admin")]
    public class ConfigurationController : Controller
    {
        //
        // GET: /Configuration/

        public ActionResult Index()
        {
            return View(ConfigLoader.SiteConfig);
        }
        
        public ActionResult SaveConfig(SiteConfig config)
        {
            if(config != null)
            {
                ConfigLoader.SiteConfig = config;
            }

            return RedirectToAction("Index");
        }

        public ActionResult SchoolThemeCSS()
        {
            var view = new CssViewResult();
            view.ViewData = new ViewDataDictionary(ConfigLoader.SiteConfig);
            return view;
        }
    }
}
