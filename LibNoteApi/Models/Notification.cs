using System;
using System.ComponentModel.DataAnnotations;

namespace LibNoteApi.Models
{
	public class Notification
	{
		[Key]
		public int Id { get; set; }

		// id from Firebase
		public string UserUid { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public DateTime DateTimeToSendEmail { get; set; }
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
		public string ZapierUrl { get; set; }
		public string SiteUrl { get; set; }
		public DateTime RecordAddedDate { get; set; }
		public bool IsSentToZapier { get; set; }
		public DateTime? SentToZapierDateTime { get; set; }
	}
}