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

            routes.MapRoute(null, "", new { controller = "Home", action = "Index", tags = "", extensions = "", page = 1 });

            routes.MapRoute(null, "page{page}",
                new { controller = "Home", action = "Index", extensions = "", tags="" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "tags/{tags}",
                new { controller = "Home", action = "Index", extensions = "", page = 1 }
            );
            routes.MapRoute(null, "tags/{tags}/page{page}",
                new { controller = "Home", action = "Index", extensions = "" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "tags/{tags}/extensions/{extensions}",
                new { controller = "Home", action = "Index", page = 1 }
            );
            routes.MapRoute(null, "tags/{tags}/extensions/{extensions}/page{page}",
                new { controller = "Home", action = "Index" },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "extensions/{extensions}",
                new { controller = "Home", action = "Index", tags = "", page = 1 }
            );
            routes.MapRoute(null, "extensions/{extensions}/page{page}",
                new { controller = "Home", action = "Index", tags = "" },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "extensions/{extensions}/tags/{tags}",
                new { controller = "Home", action = "Index", page = 1 }
            );
            routes.MapRoute(null, "extensions/{extensions}/tags/{tags}/page{page}",
                new { controller = "Home", action = "Index" },
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