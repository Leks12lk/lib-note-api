using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Tracing;
using LibNoteApi.Infrastructure;

namespace LibNoteApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// enable CORS requests
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			// Commented because we don't need detailed trace log at the moment
			//GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
