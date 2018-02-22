using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LibNoteApi.Models;
using LibNoteApi.Repositories.Interfaces;

namespace LibNoteApi.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly ApplicationDbContext _db = new ApplicationDbContext();

		public Notification GetNotification(Notification notification)
		{
			if (string.IsNullOrEmpty(notification?.UserUid) || string.IsNullOrEmpty(notification.BookTitle)) throw new ArgumentException();
			return _db.Notifications.FirstOrDefault(n => n.UserUid == notification.UserUid && n.BookTitle == notification.BookTitle);
		}

		public List<Notification> GetAllNotifications()
		{
			return _db.Notifications.ToList();
		}

		public List<Notification> GetAllNonSentNotifications()
		{
			return _db.Notifications.Where(n => n.IsSentToZapier == false).ToList();
		}

		public bool AddNotification(Notification notification)
		{
			if (notification == null) return false;

			try
			{
				_db.Notifications.Add(notification);
				_db.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateNotification(Notification notification)
		{
			if (string.IsNullOrEmpty(notification?.UserUid) || string.IsNullOrEmpty(notification.BookTitle)) return false;
			try
			{
				var existingNotif = _db.Notifications.FirstOrDefault(n => n.UserUid == notification.UserUid && n.BookTitle == notification.BookTitle);
				if (existingNotif != null)
				{
					_db.Entry(notification).State = EntityState.Modified;
					_db.SaveChanges();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteNotification(Notification notification)
		{
			if(string.IsNullOrEmpty(notification?.UserUid) || string.IsNullOrEmpty(notification.BookTitle)) return false;
			try
			{
				var existingNotif = _db.Notifications.FirstOrDefault(n => n.UserUid == notification.UserUid && n.BookTitle == notification.BookTitle);
				if (existingNotif == null) return true;

				_db.Notifications.Remove(existingNotif);
				_db.SaveChanges();

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}