using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace netZkouska.Models
{
	public class Student
	{
		public int StudentID;
		public string name;
		public string surname;

		public ICollection<Exam> Exams;
		public ICollection<StudentWork> StudentWorks;
	
	}
}