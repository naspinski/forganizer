using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forganizer.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new { controller = "Home", action = "Dashboard" });

            routes.MapRoute(null, "tags/{tags}",
                new { controller = "Files", action = "List", extensions = "", page = 1 }
            );
            routes.MapRoute(null, "tags/{tags}/page{page}",
                new { controller = "Files", action = "List", extensions = "" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "tags/{tags}/extensions/{extensions}",
                new { controller = "Files", action = "List", page = 1 }
            );
            routes.MapRoute(null, "tags/{tags}/extensions/{extensions}/page{page}",
                new { controller = "Files", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "extensions/{extensions}",
                new { controller = "Files", action = "List", tags = "", page = 1 }
            );
            routes.MapRoute(null, "extensions/{extensions}/page{page}",
                new { controller = "Files", action = "List", tags = "" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "extensions/{extensions}/tags/{tags}",
                new { controller = "Files", action = "List", page = 1 }
            );
            routes.MapRoute(null, "extensions/{extensions}/tags/{tags}/page{page}",
                new { controller = "Files", action = "List" },
                new { page = @"\d+" }
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
        }
    }
}