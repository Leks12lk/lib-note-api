using System.Web.Http;
using LibNoteApi.Models;
using LibNoteApi.Services.Interfaces;

namespace LibNoteApi.Controllers
{
	public class NotificationController : ApiController
	{
		private readonly INotificationService _notificationService;
		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public IHttpActionResult PostAddNotification(NotificationDto notification)
		{
			if (!ModelState.IsValid) return BadRequest("Model is not valid");

			var result = _notificationService.AddNotification(notification);
			if (!result) return BadRequest("Cannot save notification");

			return Ok();
		}
	}
}
