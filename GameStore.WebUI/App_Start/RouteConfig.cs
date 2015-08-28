using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "",
                new
                {
                    controller = "Game",
                    action = "GameList",
                    category = (string) null,
                    page = 1
                }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Game", action = "GameList", category = (string) null },
                constraints: new { page = @"\d+"}
                );

            routes.MapRoute(null,
                "{category}",
                new { controller = "Game", action = "GameList", page = 1 }
                );

            routes.MapRoute(null,
               "{category}/Page{page}",
               new { controller = "Game", action = "GameList" },
               new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");            
        }
    }
}
