using System.Data.Entity;

namespace LibNoteApi.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Notification> Notifications { get; set; }

		public ApplicationDbContext()
			: base("name=MainConnection")
		{
			//Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}