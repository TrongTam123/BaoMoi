using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaoMoi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Menu

           

            routes.MapRoute("Contact", "{type}/{meta}",
           new { controller = "Contact", action = "Index", meta = UrlParameter.Optional },
           new RouteValueDictionary
           {
                { "type", "lien-he" }
           },
           namespaces: new[] { "BaoMoi.Controllers" });

           routes.MapRoute("Blogs", "{type}/{meta}",
           new { controller = "Blogs", action = "Index", meta = UrlParameter.Optional },
           new RouteValueDictionary
           {
                { "type", "blogs" }
           },
           namespaces: new[] { "BaoMoi.Controllers" });
            //End Menu

            routes.MapRoute("DetailNewws", "{type}/{meta}/{id}",
            new { controller = "News", action = "DetailNewws", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "newws" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("DetailNewwsTiep", "{type}/{meta}/{id}",
            new { controller = "NewsTiep", action = "DetailNewwsTiep", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "newwstiep" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            //Trending
            routes.MapRoute("Trending", "{type}/{meta}",
            new { controller = "Trending", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.MapRoute("Detail", "{type}/{meta}/{id}",
            new { controller = "Trending", action = "Detail", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });


            //End Trending

            //News
        
            routes.MapRoute("NewNew", "{type}/{meta}",
            new { controller = "NewNew", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "news" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.MapRoute("DetailNews", "{type}/{meta}/{id}",
            new { controller = "NewNew", action = "DetailNews", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "news" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            //End News

            

            //Latest

            routes.MapRoute("Latest", "{type}/{meta}",
            new { controller = "Latest", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "latest" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.MapRoute("DetailLasts", "{type}/{meta}/{id}",
            new { controller = "Latest", action = "DetailLasts", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "latest" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            //End Latest

            //Tags

            routes.MapRoute("Tags", "{type}/{meta}",
            new { controller = "Tags", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "tags" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.MapRoute("DetailTags", "{type}/{meta}/{id}",
            new { controller = "Tags", action = "DetailTags", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "tags" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            //End Tags

            //Most

            routes.MapRoute("Most", "{type}/{meta}",
            new { controller = "Most", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "most" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            routes.MapRoute("DetailMost", "{type}/{meta}/{id}",
            new { controller = "Most", action = "DetailMost", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "most" }
            },
            namespaces: new[] { "BaoMoi.Controllers" });

            //End Most

           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BaoMoi.Controllers" }
            );
        }
    }
}
