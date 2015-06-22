using MusicSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicSchool.Controllers
{
    public class StudentController : Controller
    {
        private MusicSchoolDbContext MusicSchoolDB = new MusicSchoolDbContext();
        // GET: /Student/
        [HttpGet]
        public ActionResult ShowStudents()
        {
            IEnumerable<Student> students = MusicSchoolDB.Students.ToList(); // gets a list of students
            StudentDropDownBox();
            InstrumentsDropDownBox();
            return View(students);
        }
        
        [HttpPost]
        public ActionResult ShowStudents(FormCollection formCollection)
        {
            IEnumerable<Student> allStudents = MusicSchoolDB.Students.ToList();
            IEnumerable<Student> newStudent = new List<Student>();

            /* Add a Student */
            Student student = new Student();
            if ((formCollection["firstName"] != "") && (formCollection["LastName"] != "") && (formCollection["PhoneNumber"] != "") && (formCollection["Age"] != "")) // Checks whether the any of the fields are not empty
            {
                student.FirstName = formCollection["firstName"]; // get text from the 'firstName' text box
                student.LastName = formCollection["lastName"]; // get text from the 'lastName' text box
                student.PhoneNumber = Convert.ToInt32(formCollection["PhoneNumber"]); // get text from the 'PhoneNumber' text box
                student.Age = Convert.ToInt32(formCollection["Age"]); // get text from the 'Age' text box
                student.InstrumentID = Convert.ToInt32(formCollection["AllInstrumentsDD"]); // gets the id from the instruments drop down box
                student.OwnInstrument = Boolean.Parse(formCollection["OwnInstrumentDD"]); // gets the boolean value from the drop down box for whether a student owns an instrument

                MusicSchoolDB.Students.Add(student);
                MusicSchoolDB.SaveChanges();
            }


            /* Student Details */
            int studentID = Convert.ToInt32(formCollection["AllStudentsDD"]);
            Student DisplayStudent = new Student();
            foreach (Student s in allStudents)
            {
                if (s.StudentID == studentID)
                {
                    DisplayStudent.StudentID = studentID;
                    DisplayStudent.FirstName = s.FirstName;
                    DisplayStudent.LastName = s.LastName;
                    DisplayStudent.PhoneNumber = s.PhoneNumber;
                    DisplayStudent.Age = s.Age;
                    DisplayStudent.Instrument.Name = s.Instrument.Name;
                    DisplayStudent.OwnInstrument = s.OwnInstrument;
                }
            }

            IEnumerable<Student> students = MusicSchoolDB.Students.ToList(); // gets a list of students

            return RedirectToAction("ShowStudents", students);
        }

        /*************************************************************************************************************************************************************
                                                DROP DOWN BOXES 
        * ***********************************************************************************************************************************************************
        */

        public void StudentDropDownBox()
        {
            IEnumerable<Student> allStudents = MusicSchoolDB.Students.ToList();
            List<SelectListItem> students = new List<SelectListItem>();
            foreach (Student s in allStudents)
            {
                students.Add(new SelectListItem { Text = s.FirstName + " " + s.LastName, Value = s.StudentID.ToString() });
            }
            ViewBag.AllStudentsDD = students;
        }

        public void InstrumentsDropDownBox()
        {
            IEnumerable<Instrument> Instruments = MusicSchoolDB.Instruments.ToList();
            List<SelectListItem> instruments = new List<SelectListItem>();
            foreach (Instrument i in Instruments)
            {
                instruments.Add(new SelectListItem { Text = i.Name, Value = i.InstrumentID.ToString() });
            }
            ViewBag.AllInstrumentsDD = instruments;
        }

        /* Page that shows all the details for one student */
        public ActionResult Details(int studentID)
        {
            Student current = MusicSchoolDB.Students.Find(studentID); // finds one student and passes it to the view
            return View("Details", current);
        }
	}
}