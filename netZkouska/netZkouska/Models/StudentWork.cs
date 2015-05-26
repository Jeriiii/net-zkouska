using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace netZkouska.Models
{
	public class StudentWork
	{
		public int StudentWorkID { get; set; }
		public int Points { get; set; }
		public string Commnent { get; set; }
		public DateTime WorkDate { get; set; }
	}
}