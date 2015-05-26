using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using netZkouska.Models;

namespace netZkouska.DAL
{
	
		public class DbInit : System.Data.Entity.DropCreateDatabaseAlways<StudentNet>
		{

			protected override void Seed(StudentNet context)
			{
				var studentWorks = new List<StudentWork>
				{
				new StudentWork{Points=1050,Commnent="Chemistry",WorkDate=DateTime.Parse("2015-01-01")},

				};

				//studentWorks.ForEach(s => context.StudentWorks.Add(s));
				//	context.SaveChanges();
				//	var enrollments = new List<Enrollment>

				var students = new List<Student>
				{
				new Student{Name="Carson",Surname="Alexander", StudentWorks=studentWorks},
				new Student{Name="Neal",Surname="Jackson"},
				new Student{Name="Pepa",Surname="Omáčka"},
				new Student{Name="Honza",Surname="Skočdopole"}
				};

				students.ForEach(s => context.Students.Add(s));
				context.SaveChanges();
				
				
					
				//{
				//new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
				//new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
				//new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
				//new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
				//new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
				//new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
				//new Enrollment{StudentID=3,CourseID=1050},
				//new Enrollment{StudentID=4,CourseID=1050,},
				//new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
				//new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
				//new Enrollment{StudentID=6,CourseID=1045},
				//new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
				//};
				//	enrollments.ForEach(s => context.Enrollments.Add(s));
				//	context.SaveChanges();
			}
		}
}