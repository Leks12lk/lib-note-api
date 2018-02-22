using System;
using System.Configuration;
using LibNoteApi.Models;
using LibNoteApi.Repositories.Interfaces;
using LibNoteApi.Services.Interfaces;

namespace LibNoteApi.Services
{
	public class NotificationService : INotificationService
	{
		private readonly INotificationRepository _notificationRepository;
		public NotificationService(INotificationRepository notificationRepository)
		{
			_notificationRepository = notificationRepository;
		}

		public bool AddNotification(NotificationDto dto)
		{
			var notification = MapDtoToNotification(dto);
			return _notificationRepository.AddNotification(notification);
		}

		public bool UpdateNotification(NotificationDto dto)
		{
			var notification = MapDtoToNotification(dto);
			return _notificationRepository.UpdateNotification(notification);
		}

		public bool IsExistNotification(NotificationDto dto)
		{
			var notification = MapDtoToNotification(dto);
			return _notificationRepository.GetNotification(notification) != null;
		}

		public bool DeleteNotification(NotificationDto dto)
		{
			var notification = MapDtoToNotification(dto);
			return _notificationRepository.DeleteNotification(notification);
		}

		private static Notification MapDtoToNotification(NotificationDto dto)
		{
			string zapierHookUrl = ConfigurationManager.AppSettings["ZapierHookUrl"];
			string libNoteUrl = ConfigurationManager.AppSettings["LibNoteUrl"];

			return new Notification
			{
				Email = dto.Email,
				UserName = dto.UserName,
				UserUid = dto.UserUid,
				BookTitle = dto.BookTitle,
				BookAuthor = dto.BookAuthor,
				DateTimeToSendEmail = dto.DateTimeToSendEmail,
				RecordAddedDate = DateTime.UtcNow,
				ZapierUrl = zapierHookUrl,
				SiteUrl = libNoteUrl
			};
		}
	}
}