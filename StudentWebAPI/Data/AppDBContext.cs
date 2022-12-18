using Microsoft.EntityFrameworkCore;
using StudentWebAPI.Model;

namespace StudentWebAPI.Data
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options)
			: base(options)
		{

		}
		public DbSet<Student> Studentdata { get; set; }

		public DbSet<Subject> Subjectdata { get; set; }
	}
}
