using LibNoteApi.Models;

namespace LibNoteApi.Services.Interfaces
{
	public interface INotificationService
	{
		bool AddNotification(NotificationDto dto);
		bool UpdateNotification(NotificationDto dto);
		bool IsExistNotification(NotificationDto dto);
		bool DeleteNotification(NotificationDto dto);
	}
}
