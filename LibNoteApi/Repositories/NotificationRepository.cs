using System;
using System.Collections.Generic;
using System.Linq;
using LibNoteApi.Models;
using LibNoteApi.Repositories.Interfaces;
using NLog;

namespace LibNoteApi.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly ApplicationDbContext _db = new ApplicationDbContext();
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public Notification GetNotification(Notification notification)
		{
			if (string.IsNullOrEmpty(notification?.UserUid) || string.IsNullOrEmpty(notification.BookKey)) throw new ArgumentException();
			return _db.Notifications.FirstOrDefault(n => n.UserUid == notification.UserUid && n.BookKey == notification.BookKey);
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
			if (notification == null) throw new ArgumentException("Notification is null");

			Logger.Info($"AddNotification started for BookKey {notification.BookKey} and UserUid {notification.UserUid}");

			var existingNotifications = _db.Notifications.Where(n => n.UserUid == notification.UserUid
			                                                        && n.BookKey == notification.BookKey
			                                                        && !n.IsSentToZapier);

			if (existingNotifications.Any())
			{
				Logger.Info($"Any notification is already exist with BookKey {notification.BookKey} " +
				            $"and UserUid {notification.UserUid}. And not sent to Zapier yet");
				return true;
			}

			_db.Notifications.Add(notification);
			_db.SaveChanges();

			Logger.Info($"AddNotification completed for BookKey {notification.BookKey} and UserUid {notification.UserUid}");

			return true;
		}

		public bool UpdateNotification(Notification notification)
		{
			if (string.IsNullOrEmpty(notification?.UserUid)) throw new ArgumentException("UserUid is empty");
			if (string.IsNullOrEmpty(notification.BookKey)) throw new ArgumentException("BookKey is empty");

			// update only not sent to Zapier notification
			var existingNotif = _db.Notifications.FirstOrDefault(n => n.UserUid == notification.UserUid 
								&& n.BookKey == notification.BookKey
								&& !n.IsSentToZapier);

			if (existingNotif != null)
			{
				Logger.Info($"UpdateNotification started for BookKey {notification.BookKey} " +
				            $"and UserUid {notification.UserUid}");

				existingNotif.UserUid = notification.UserUid;
				existingNotif.Email = notification.Email;
				existingNotif.UserName = notification.UserName;
				existingNotif.DateTimeToSendEmail = notification.DateTimeToSendEmail;
				existingNotif.BookKey = notification.BookKey;
				existingNotif.BookTitle = notification.BookTitle;
				existingNotif.BookAuthor = notification.BookAuthor;

				_db.SaveChanges();

				Logger.Info($"UpdateNotification completed for BookKey {notification.BookKey} " +
				            $"and UserUid {notification.UserUid}");
			}
			else
			{
				Logger.Info($"UpdateNotification. No existing notifications with BookKey {notification.BookKey} " +
				           $"and UserUid {notification.UserUid}");
			}
			return true;
		}

		public bool DeleteNotification(string bookKey, string userUid)
		{
			if (string.IsNullOrEmpty(bookKey)) throw new ArgumentException("bookKey is empty");
			if (string.IsNullOrEmpty(userUid)) throw new ArgumentException("userUid is empty");

			// delete only not sent to Zapier notification
			var existingNotif = _db.Notifications.FirstOrDefault(n => n.UserUid == userUid
								&& n.BookKey == bookKey
								&& !n.IsSentToZapier);

			if (existingNotif == null)
			{
				Logger.Info($"DeleteNotification. No existing notifications with BookKey {bookKey} " +
				            $"and UserUid {userUid}");
				return true;
			}

			Logger.Info($"DeleteNotification started for BookKey {bookKey} " +
			            $"and UserUid {userUid}");

			_db.Notifications.Remove(existingNotif);
			_db.SaveChanges();

			Logger.Info($"DeleteNotification completed for BookKey {bookKey} " +
			            $"and UserUid {userUid}");

			return true;
		}
	}
}