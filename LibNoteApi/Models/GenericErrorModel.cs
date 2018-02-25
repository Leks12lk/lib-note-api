using System;

namespace LibNoteApi.Models
{
	public class GenericErrorModel
	{
		public string Message { get; set; }
		public Guid ErrorId { get; set; }

		/// <summary>
		/// Will be populated onliy IF DEBUG!
		/// </summary>
		public string StackTrace { get; set; }
	}
}