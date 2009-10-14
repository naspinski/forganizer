using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Forganizer.WebUI.App_Code;

namespace Forganizer.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new { controller = "Search", action = "Index", tags = "", extensions = "", page = 1 });

            routes.MapRoute(null, "Page{page}",
                new { controller = "Search", action = "Index", extensions = "", tags="" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "Tags/{tags}",
                new { controller = "Search", action = "Index", extensions = "", page = 1 }
            );
            routes.MapRoute(null, "Tags/{Tags}/Page{page}",
                new { controller = "Search", action = "Index", extensions = "" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "Tags/{tags}/Extensions/{extensions}",
                new { controller = "Search", action = "Index", page = 1 }
            );
            routes.MapRoute(null, "Tags/{tags}/Extensions/{extensions}/Page{page}",
                new { controller = "Search", action = "Index" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "Extensions/{extensions}",
                new { controller = "Search", action = "Index", tags = "", page = 1 }
            );
            routes.MapRoute(null, "Extensions/{extensions}/Page{page}",
                new { controller = "Search", action = "Index", tags = "" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/", new { action = "Index" });
            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
        }
    }
}