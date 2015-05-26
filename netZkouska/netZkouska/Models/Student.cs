using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace netZkouska.Models
{
	public class Student
	{
		public int StudentID { get; set; }
		public string name { get; set; }
		public string surname { get; set; }

		[NotMapped]
		public string hasCourseCredit { get {
			foreach (StudentWork sw in StudentWorks)
			{
				if (sw.Points > 45)
				{
					return "Má";
				}
			}
			return "Nemá";
		}
		}

		public virtual ICollection<Exam> Exams { get; set; }
		public virtual ICollection<StudentWork> StudentWorks {get; set;}
	
	}
}