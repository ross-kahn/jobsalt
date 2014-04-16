using jobSalt.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace jobSalt
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs args)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.User != null)
                {
                    bool isAdmin = AccountController.IsAdmin(HttpContext.Current.User.Identity.Name);
                    String[] roles = isAdmin ? new string[] { "admin" } : null;

                    GenericPrincipal principal = new GenericPrincipal(HttpContext.Current.User.Identity, roles);

                    HttpContext.Current.User = principal;
                    System.Threading.Thread.CurrentPrincipal = System.Web.HttpContext.Current.User;
                }
            }
        }
    }
}