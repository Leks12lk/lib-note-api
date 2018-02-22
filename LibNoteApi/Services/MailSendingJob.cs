using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using FluentScheduler;
using LibNoteApi.Models;
using LibNoteApi.Repositories;

namespace LibNoteApi.Services
{
	public class MailSendingJob : IJob, IRegisteredObject
	{
		//private readonly INotificationRepository _notificationRepository;
		private readonly object _lock = new object();

		private bool _shuttingDown;

		public MailSendingJob()
		{
			// Register this job with the hosting environment.
			// Allows for a more graceful stop of the job, in the case of IIS shutting down.
			HostingEnvironment.RegisterObject(this);

			//_notificationRepository = notificationRepository;
		}

		public void Execute()
		{
			try
			{
				var notificationRepo = new NotificationRepository();
				var notifications = notificationRepo.GetAllNonSentNotifications();
				lock (_lock)
				{
					if (_shuttingDown)
						return;

					// Do work, son!
					foreach (var notification in notifications)
					{
						var shouldSendEmail = (DateTime.UtcNow - notification.DateTimeToSendEmail).Seconds >= 0;
						SendRequestToZapier(shouldSendEmail, notification, notificationRepo);
					}
				}
			}
			finally
			{
				// Always unregister the job when done.
				HostingEnvironment.UnregisterObject(this);
			}
		}

		public void Stop(bool immediate)
		{
			// Locking here will wait for the lock in Execute to be released until this code can continue.
			lock (_lock)
			{
				_shuttingDown = true;
			}

			HostingEnvironment.UnregisterObject(this);
		}

		private void SendRequestToZapier(bool shouldSendEmail, Notification notification, NotificationRepository notificationRepository)
		{
			if (!shouldSendEmail) return;

			var queryString = notification.ZapierUrl;
			queryString +=
				$"?email={notification.Email}&bookTitle={notification.BookTitle}&userName={notification.UserName}&siteUrl={notification.SiteUrl}";

			HttpClient client = new HttpClient();
			// Add an Accept header for JSON format.
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage response = client.GetAsync(queryString).Result;

			if (response.IsSuccessStatusCode)
			{
				notification.IsSentToZapier = true;
				notification.SentToZapierDateTime = DateTime.UtcNow;
				notificationRepository.UpdateNotification(notification);
			}
		}


	}
}