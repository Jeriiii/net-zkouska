using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using netZkouska.Models;
using System.Xml.Linq;

namespace netZkouska.DAL
{
	
	public class DbInit : System.Data.Entity.DropCreateDatabaseAlways<StudentNet>
	{

		protected override void Seed(StudentNet context)
		{
			SaveToXml();
			List<Student> students = LoadToXml();


			//var studentWorks = new List<StudentWork>
			//{
			//new StudentWork{Points=1050,WorkDate=DateTime.Parse("2015-01-01")},

			//};

			//var exams = new List<Exam>
			//{
			//new Exam{Points=900,ExamDate=DateTime.Parse("2015-01-01")},

			//};

			////studentWorks.ForEach(s => context.StudentWorks.Add(s));
			////	context.SaveChanges();
			////	var enrollments = new List<Enrollment>

			//var students = new List<Student>
			//{
			//new Student{Name="Carson",Surname="Alexander", StudentWorks=studentWorks, Exams=exams},
			//new Student{Name="Neal",Surname="Jackson"},
			//new Student{Name="Pepa",Surname="Omáčka"},
			//new Student{Name="Honza",Surname="Skočdopole"}
			//};

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

		public void SaveToXml()
		{
			// vytvor dokument
			XDocument doc = new XDocument();
			
			// vytvor element "Studenti" a pridej do dokumentu
			XElement xmlStudents = new XElement("Studenti");
			doc.Add(xmlStudents);

			var students = new List<Student>
			{
			new Student{Name="Carson",Surname="Alexander"},
			new Student{Name="Neal",Surname="Jackson"},
			new Student{Name="Pepa",Surname="Omáčka"},
			new Student{Name="Honza",Surname="Skočdopole"}
			};

			Random r = new Random();
			// pro kazdeho studenta vytvor element a vnorene elementy a napln hodnotami
			foreach (var student in students)
			{
				XElement exams = new XElement("Zkousky");
				
				exams.Add(
					new XElement("Zkouska", 
						new XElement("body", r.Next(1600)),
						new XElement("datum", DateTime.Parse("2015-01-01").ToString())
					)
				);

				xmlStudents.Add(new XElement("Student",
					new XElement("Jmeno", student.Name),
					new XElement("Prijmeni", student.Surname),
					exams
				));
			}

			doc.Save("D:\\Documents\\Projekty\\data.xml");
		}
				  
		public List<Student> LoadToXml()
		{
			List<Student> students =new List<Student>();

			// vytvor dokument
			XDocument configXML = XDocument.Load("D:\\Documents\\Projekty\\data.xml");

			// vytvor element "Studenti" a pridej do dokumentu
			var xmlStudents = from c in configXML.Descendants("Studenti").Descendants("Student") select c;
			

			// pro kazdeho studenta vytvor element a vnorene elementy a napln hodnotami
			foreach (var student in xmlStudents)
			{
				string name = (string) student.Descendants("Jmeno").FirstOrDefault();
				string surname = (string)student.Descendants("Prijmeni").FirstOrDefault();

				List<Exam> exams = new List<Exam>();

				var xmlExams = from c in student.Descendants("Zkousky").Descendants("Zkouska") select c;
				foreach (var exam in xmlExams) {
					int points = (int)exam.Descendants("body").FirstOrDefault();
					DateTime date = DateTime.Parse(
						(string)exam.Descendants("datum").FirstOrDefault()
					);

					Exam e = new Exam { Points = points, ExamDate = date };

					exams.Add(e);
				}

				Student st = new Student { Name = name, Surname = surname, Exams = exams};

				students.Add(st);
			}

			return students;
		}
	}
}