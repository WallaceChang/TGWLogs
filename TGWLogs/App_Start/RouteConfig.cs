using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TGWLogs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "TlogSendState",
                url: "TGW/TlogSendState/{TxDateST}/{TxDateED}/{PageIdx}/{CardNo}",
                defaults: new { Controller = "TGW", action = "TlogSendState", PageIdx = UrlParameter.Optional, CardNo = UrlParameter.Optional });

            routes.MapRoute(
                name: "TX300SendState",
                url: "TGW/TX300SendState/{SendDateST}/{SendDateED}/{PageIdx}/{CardNo}",
                defaults: new { Controller = "TGW", Action = "TX300SendState", PageIdx = UrlParameter.Optional, CardNo = UrlParameter.Optional });

            routes.MapRoute(
                name: "LogFilesList",
                url: "TGW/LogFilesList/{FileSource}",
                defaults: new { controller = "TGW", action = "LogFilesList" });

            routes.MapRoute(
                name: "SysList",
                url: "TGW/SysList/{PageIdx}/{GrpName}/{SysName}",
                defaults: new { controller = "TGW", Action = "SysList", PageIdx = UrlParameter.Optional, GrpName = UrlParameter.Optional, SysName = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TGW", action = "Index", id = UrlParameter.Optional });
        }
    }
}
