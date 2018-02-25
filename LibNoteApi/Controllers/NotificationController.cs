using System;
using System.Web.Http;
using LibNoteApi.Models;
using LibNoteApi.Services.Interfaces;
using NLog;

namespace LibNoteApi.Controllers
{
	public class NotificationController : ApiController
	{
		private readonly INotificationService _notificationService;
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpPost]
		public IHttpActionResult PostAddNotification(NotificationDto notification)
		{
			if (!ModelState.IsValid) return BadRequest("Model is not valid");

			Logger.Info($"Started adding new notification with BookKey {notification.BookKey} " +
			            $"and UserUid {notification.UserUid}");

			var result = _notificationService.AddNotification(notification);
			if (!result)
			{
				return BadRequest("Cannot save notification");
			}

			return Ok();
		}

		[HttpPut]
		public IHttpActionResult PutUpdateNotification(NotificationDto notification)
		{
			if (!ModelState.IsValid) return BadRequest("Model is not valid");

			Logger.Info($"Started updating notification with BookKey {notification.BookKey} " +
			            $"and UserUid {notification.UserUid}");

			var result = _notificationService.UpdateNotification(notification);
			if (!result)
			{
				return BadRequest("Cannot update notification");
			}

			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteNotification(string bookKey, string userUid)
		{
			if (string.IsNullOrEmpty(bookKey)) throw new ArgumentException("bookKey is empty");
			if (string.IsNullOrEmpty(userUid)) throw new ArgumentException("userUid is empty");

			Logger.Info($"Started deleting notification with BookKey {bookKey} " +
			            $"and UserUid {userUid}");

			var result = _notificationService.DeleteNotification(bookKey, userUid);
			if (!result)
			{
				return BadRequest("Cannot delete notification");
			}

			return Ok();
		}
	}
}
