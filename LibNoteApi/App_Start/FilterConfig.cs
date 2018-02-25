using System.Web.Http;
using System.Web.Mvc;
using LibNoteApi.Attributes;

namespace LibNoteApi
{
	public class FilterConfig
	{
		//public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		//{
		//	//filters.Add(new HandleErrorAttribute());
		//	filters.Add(new ApiExceptionHandlerFilterAttribute());
		//}

		public static void RegisterGlobalFilters(HttpConfiguration configuration)
		{
			configuration.Filters.Add(new ApiExceptionHandlerFilterAttribute());
		}
	}
}
