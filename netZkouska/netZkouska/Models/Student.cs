using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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

			XDocument configXML = XDocument.Load("D:\\Documents\\Projekty\\config.xml");

			var configQuery = from c in configXML.Descendants("configs").Descendants("grades")
							 select c;// getting ship node in ships XML

			int onePoints = 0;
			int twoPoints = 0;
			int threePoints = 0;

			foreach(XElement g in configQuery) {
				onePoints = (int) g.Descendants("one").FirstOrDefault();
				twoPoints = (int)g.Descendants("two").FirstOrDefault();
				threePoints = (int)g.Descendants("three").FirstOrDefault();
			}


			int bestPoints = 0;
			foreach(Exam e in Exams) {
				if (e.Points > bestPoints)
				{
					bestPoints = e.Points;
				}
			}

			if (bestPoints >= onePoints)
			{
				return 1;
			}
			if (bestPoints >= twoPoints)
			{
				return 2;
			}
			if (bestPoints >= threePoints)
			{
				return 3;
			}
			return 4;
		} }

		public virtual ICollection<Exam> Exams { get; set; }
		public virtual ICollection<StudentWork> StudentWorks {get; set;}
	
	}
}