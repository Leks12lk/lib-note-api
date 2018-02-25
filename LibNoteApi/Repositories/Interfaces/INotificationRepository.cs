using System.Collections.Generic;
using LibNoteApi.Models;

namespace LibNoteApi.Repositories.Interfaces
{
	public interface INotificationRepository
	{
		Notification GetNotification(Notification notification);
		List<Notification> GetAllNotifications();
		List<Notification> GetAllNonSentNotifications();
		bool AddNotification(Notification notification);
		bool UpdateNotification(Notification notification);
		bool DeleteNotification(string bookKey, string userUid);
	}
}
