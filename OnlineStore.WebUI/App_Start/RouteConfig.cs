using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { controller = "Customer", action = "Welcome", customerCode = (string)null, saleId = (string)null }
            );

            routes.MapRoute(
                null,
                "{customerCode}",
                new { controller = "Customer", action = "Welcome" }
            );

            routes.MapRoute(
                null,
                "{customerCode}/sales/{saleId}/{itemType}",
                new { controller = "Sale", action = "List" }
            );

            routes.MapRoute(
                null,
                "{customerCode}/sales/{saleId}",
                new { controller = "Sale", action = "List", itemType = (string)null }
            );

            routes.MapRoute(
                null,
                "{controller}/{action}"
            );
        }
    }
}
