using System;
using System.ComponentModel.DataAnnotations;

namespace LibNoteApi.Models
{
	public class NotificationDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string UserUid { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
		[Required]
		public DateTime DateTimeToSendEmail { get; set; }

	}
}