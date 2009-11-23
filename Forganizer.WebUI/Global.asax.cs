using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Forganizer.WebUI.App_Code;
using Forganizer.WebUI.Models;
using Forganizer.DomainModel.Entities;

namespace Forganizer.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new { controller = "Search", action = "Index", tags = "", extensions = "", page = 1, tagAndOr = SearchType.And });
            
            routes.MapRoute(null, "Page{page}",
                new { controller = "Search", action = "Index", extensions = "", tags="", tagAndOr=SearchType.And },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}/Page{page}",
                new { controller = "Search", action = "Index", extensions = "", tags="", page = 1 },
                new { page = @"\d+", tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}/{tags}",
                new { controller = "Search", action = "Index", extensions = "", page = 1 },
                new { tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}",
                new { controller = "Search", action = "Index", extensions = "", page = 1 },
                new { tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Tags/{tags}",
                new { controller = "Search", action = "Index", extensions = "", page = 1, tagAndOr = SearchType.And }
            );
            routes.MapRoute(null, "Tags/{Tags}/Page{page}",
                new { controller = "Search", action = "Index", extensions = "", tagAndOr = SearchType.And },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}/{Tags}/Page{page}",
                new { controller = "Search", action = "Index", extensions = "" },
                new { page = @"\d+", tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Tags/{tags}/Extensions/{extensions}",
                new { controller = "Search", action = "Index", page = 1, tagAndOr = SearchType.And }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}/{tags}/Extensions/{extensions}",
                new { controller = "Search", action = "Index", page = 1 },
                new { tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Tags/{tags}/Extensions/{extensions}/Page{page}",
                new { controller = "Search", action = "Index", tagAndOr = SearchType.And },
                new { page = @"\d+" }
            );
            routes.MapRoute(null, "Tags/{tagAndOr}/{tags}/Extensions/{extensions}/Page{page}",
                new { controller = "Search", action = "Index" },
                new { page = @"\d+", tagAndOr = @"And|Or{1}" }
            );
            routes.MapRoute(null, "Extensions/{extensions}",
                new { controller = "Search", action = "Index", tags = "", page = 1, tagAndOr = SearchType.And }
            );
            routes.MapRoute(null, "Extensions/{extensions}/Page{page}",
                new { controller = "Search", action = "Index", tags = "", tagAndOr = SearchType.And },
                new { page = @"\d+" }
            );

            routes.MapRoute(null, "Delete/{Id}",
                new { controller = "Search", action = "Delete", returnTo = "/" },
                new { Id = @"\d+" }
            );
            routes.MapRoute(null, "Delete/{Id}/ReturnTo/{*returnTo}", 
                new { controller = "Search", action = "Delete", returnTo = "" },
                new { Id = @"\d+" }
            );

            routes.MapRoute(null, "DeleteTag/{tag}/From/{Id}",
                new { controller = "Search", action = "DeleteTag", returnTo = "/" },
                new { Id = @"\d+" }
            );
            routes.MapRoute(null, "DeleteTag/{tag}/From/{Id}/ReturnTo/{*returnTo}",
                new { controller = "Search", action = "DeleteTag", returnTo = "" },
                new { Id = @"\d+" }
            );

            routes.MapRoute(null, "AddTags",
                new { controller = "Search", action = "AddTags", returnTo = "/" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );
            routes.MapRoute(null, "Submit/ReturnTo/{*returnTo}",
                new { controller = "Search", action = "AddTags", returnTo = "" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );

            routes.MapRoute(null, "Manage/Tags/", new { controller = "Manage", action = "Tags", tagEditType = TagEditType.Edit });
            routes.MapRoute(null, "Manage/Tags/{tagEditType}", new { controller = "Manage", action = "Tags"});


            routes.MapRoute(null, "{controller}/{action}/{extension}/From/{Id}");

            routes.MapRoute(null, "{controller}/{action}/{id}");
            routes.MapRoute(null, "{controller}/", new { action = "Index" });

            routes.MapRoute(null, "{controller}/Submit",
                new { formPost = true, action = "Index" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );
            routes.MapRoute(null, "{controller}/Submit/ReturnTo/{*returnTo}",
                new { formPost = true, action = "Index" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );
            routes.MapRoute(null, "{controller}/{action}");

            routes.MapRoute(null, "{*url}", new { controller = "Error", action = "NotFound" });
            routes.MapRoute(null, "Error/NotFound/{*url}", new { controller = "Error", action = "NotFound" });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
        }
    }
}