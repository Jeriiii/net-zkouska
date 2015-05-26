using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace netZkouska.Models
{
	public class Student
	{
		public int StudentID { get; set; }
		public string Name { get; set; }
		[MaxLength(80)]
		public string Surname { get; set; }

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

		[NotMapped]
		public int Grade { get {
			int bestPoints = 0;
			foreach(Exam e in Exams) {
				if (e.Points > bestPoints)
				{
					bestPoints = e.Points;
				}
			}

			if (bestPoints >= 950)
			{
				return 1;
			}
			if (bestPoints >= 850)
			{
				return 2;
			}
			if (bestPoints >= 250)
			{
				return 3;
			}
			return 4;
		} }

		public virtual ICollection<Exam> Exams { get; set; }
		public virtual ICollection<StudentWork> StudentWorks {get; set;}
	
	}
}