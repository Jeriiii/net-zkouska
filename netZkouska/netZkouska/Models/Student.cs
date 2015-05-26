using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace netZkouska.Models
{
	public class Student
	{
		public int StudentID { get; set; }
		public string name { get; set; }
		public string surname { get; set; }

		public virtual ICollection<Exam> Exams { get; set; }
		public virtual ICollection<StudentWork> StudentWorks {get; set;}
	
	}
}