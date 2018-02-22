using FluentScheduler;

namespace LibNoteApi.Services
{
	public class MailSendingRegistry : Registry
	{
		public MailSendingRegistry()
		{
			// Schedule an IJob to run at an interval
			Schedule<MailSendingJob>().ToRunNow().AndEvery(1).Minutes();

			//// Schedule an IJob to run once, delayed by a specific time interval
			//Schedule<MailSendingJob>().ToRunOnceIn(5).Seconds();

			//// Schedule a simple job to run at a specific time
			//Schedule(() => Console.WriteLine("It's 9:15 PM now.")).ToRunEvery(1).Days().At(21, 15);

			//// Schedule a more complex action to run immediately and on an monthly interval
			//Schedule<MailSendingJob>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);
			
		}
	}
}