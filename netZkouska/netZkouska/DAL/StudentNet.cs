namespace netZkouska.DAL
{
	using netZkouska.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Linq;

	public class StudentNet : DbContext
	{
		public StudentNet()
			: base("name=StudentNet")
		{
		}

		public DbSet<Exam> Exams { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<StudentWork> StudentWorks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	//public class MyEntity
	//{
	//    public int Id { get; set; }
	//    public string Name { get; set; }
	//}
}