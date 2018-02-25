using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using LibNoteApi.Models;
using NLog;

namespace LibNoteApi.Attributes
{
	public class ApiExceptionHandlerFilterAttribute : ExceptionFilterAttribute
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private const string GenericErrorText = "Ooooops, something went wrong. Please contact support for help and tell following ErrorID {0}";
	

		public override void OnException(HttpActionExecutedContext context)
		{
			Guid errorId = Guid.NewGuid();
			Logger.Error(context.Exception, $"Unhandled error. ErrorId is {errorId}. Controller is {context.ActionContext.ControllerContext.Controller}. StackTrace: {context.Exception.StackTrace}");

			var response = new GenericErrorModel
			{
				Message = string.Format(GenericErrorText, errorId),
				ErrorId = errorId,
#if DEBUG
				StackTrace = context.Exception.StackTrace,
#endif
			};

			context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
			context.Exception = null;
		}
	}
}