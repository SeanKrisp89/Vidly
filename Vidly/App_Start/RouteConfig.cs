using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//In order to use attribute routing, you need to enable it
			routes.MapMvcAttributeRoutes();
			//routes.MapRoute(
			//	"MoviesByReleaseDate",
			//	"movies/released/{year}/{month}",
			//	new { controller = "Movies", Action = "ByReleaseDate" },
			//	new { year = @"\d{4}", month = @"\d{2}" });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
