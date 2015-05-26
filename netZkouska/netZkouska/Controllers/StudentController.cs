using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using netZkouska.DAL;
using netZkouska.Models;
using System.IO;
using System.Text;

namespace netZkouska.Controllers
{
	public class StudentController : Controller
	{
		private StudentNet db = new StudentNet();

		// GET: Student
		public ActionResult Index(string sortOrder)
		{
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.SurNameSortParm = sortOrder == "surName" ? "surName_desc" : "surName";
			ViewBag.HasCourseSortParm = sortOrder == "hasCourse" ? "hasCourse_desc" : "hasCourse";
			ViewBag.GradeParm = sortOrder == "grade" ? "grade_desc" : "grade";

			var students = from s in db.Students
						   select s;

			List<Student> std;
			switch (sortOrder)
			{
				case "name_desc":
					students = students.OrderByDescending(s => s.Name);
					break;
				case "surName":
					students = students.OrderBy(s => s.Surname);
					break;
				case "surName_desc":
					students = students.OrderByDescending(s => s.Surname);
					break;
				case "hasCourse":
					std = students.ToList();
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.hasCourseCredit.Equals(s2.hasCourseCredit) ? 1 : 0;
						}
					);
					return View(std);
					break;
				case "hasCourse_desc":
					std = students.ToList();
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.hasCourseCredit.Equals(s2.hasCourseCredit) ? 0 : 1;
						}
					);
					return View(std);
					break;
				case "grade":
					std = students.ToList();
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.Grade > s2.Grade ? 1 : -1;
						}
					);
					return View(std);
					break;
				case "grade_desc":
					std = students.ToList();
					std.Sort(
						delegate(Student s1, Student s2)
						{
							return s1.Grade > s2.Grade ? -1 : 1;
						}
					);
					return View(std);
					break;
				default:
					students = students.OrderBy(s => s.Name);
					break;
			}


			//TextWriter tw = Response.Output;
			//Response.Output = new StreamWriter("D:\\Documents\\Projekty\\output.html");

			//string myString = RenderRazorViewToString("~/Views/Student/Index.cshtml", View(db.Students.ToList()).Model);

			//System.IO.File.WriteAllText("D:\\Documents\\Projekty\\output.html", myString);

			//Response.Output = tw;

			return View(students.ToList());
		}

		public string RenderRazorViewToString(string viewName, object model)
		{
			ViewData.Model = model;
			using (var sw = new StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
																		 viewName);
				var viewContext = new ViewContext(ControllerContext, viewResult.View,
											 ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				return sw.GetStringBuilder().ToString();
			}
		}

		// GET: Student/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// GET: Student/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Student/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "StudentID,Name,Surname")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Students.Add(student);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(student);
		}

		// GET: Student/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// POST: Student/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "StudentID,Name,Surname")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Entry(student).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(student);
		}

		// GET: Student/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		// POST: Student/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Student student = db.Students.Find(id);
			db.Students.Remove(student);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
