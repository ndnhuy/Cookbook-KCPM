using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cookbook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Users",
                url: "user/{username}/{action}",
                defaults: new
                {
                    controller = "User",
                    action = "Index",
                    username = ""
                }
            );

            routes.MapRoute(
                name: "Dish",
                url: "dish/{id}",
                defaults: new
                {
                    controller = "Dish",
                    action = "Index",
                    id = ""
                }
            );

            routes.MapRoute(
                name: "Search",
                url: "search/{id}",
                defaults: new
                {
                    controller = "Search",
                    action = "Index",
                    id = ""
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new 
                { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional 
                }
            );
        }
    }
}