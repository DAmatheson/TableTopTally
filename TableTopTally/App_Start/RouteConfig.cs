using System.Web.Mvc;
using System.Web.Routing;
using TableTopTally.RouteConstraints;

namespace TableTopTally
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Any request with ?angular=true will be routed through this
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                constraints: new
                {
                    angular = new QueryStringConstraint("true")
                }
            );

            // All other requests will be routed to the angular index page for angular to handle
            routes.MapRoute(
                name: "AngularCatchall",
                url: "{*catchall}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );
        }
    }
}
