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
        
        public ActionResult SaveConfig(SiteConfig config, JobConfig jobConfig, AlumniConfig alumniConfig, AuthenticationConfig authConfig)
        {
            if(config != null)
            {
                var oldConfig = ConfigLoader.SiteConfig;

                // Check if the passwords are the placeholder passwords, if so save the orignal value
                if (config.JobsDBConnection.Password == "PasswordJS")
                    config.JobsDBConnection.Password = oldConfig.JobsDBConnection.Password;

                if (config.AlumniDBConnection.Password == "PasswordJS")
                    config.AlumniDBConnection.Password = oldConfig.AlumniDBConnection.Password;

                if (config.SalaryDBConnection.Password == "PasswordJS")
                    config.SalaryDBConnection.Password = oldConfig.SalaryDBConnection.Password;

                if (config.HousingDBConnection.Password == "PasswordJS")
                    config.HousingDBConnection.Password = oldConfig.HousingDBConnection.Password;

                ConfigLoader.SiteConfig = config;
            }

            if(jobConfig != null)
            {
                var oldConfig = ConfigLoader.JobConfig;
                oldConfig.Modules = jobConfig.Modules;

                ConfigLoader.JobConfig = oldConfig;
            }

            if(authConfig != null)
            {
                var oldConfig = ConfigLoader.AuthenticationConfig;
                oldConfig.AdminUsers = new List<string>();
                foreach(string user in authConfig.AdminUsers[0].Split('\n'))
                {
                    oldConfig.AdminUsers.Add(user.Trim());
                }

                oldConfig.RestrictAccessToUsers = new List<string>();
                foreach (string user in authConfig.RestrictAccessToUsers[0].Split('\n'))
                {
                    oldConfig.RestrictAccessToUsers.Add(user);
                }

                ConfigLoader.AuthenticationConfig = oldConfig;
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult SchoolThemeCSS()
        {
            var view = new CssViewResult();
            view.ViewData = new ViewDataDictionary(ConfigLoader.SiteConfig);
            return view;
        }
    }
}
