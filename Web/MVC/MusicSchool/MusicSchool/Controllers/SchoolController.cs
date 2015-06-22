using MusicSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicSchool.Controllers
{
    public class SchoolController : Controller
    {
        private MusicSchoolDbContext MusicSchoolDB = new MusicSchoolDbContext();

        [HttpGet]
        public ActionResult Enrolment()
        {
            IEnumerable<Student> students = MusicSchoolDB.Students.ToList(); // gets a list of students
            InstrumentsDropDownBox();
            return View(students);
        }

        [HttpPost]
        public ActionResult Enrolment(FormCollection formCollection)
        {
            int newInstrument = Convert.ToInt32(formCollection["AllInstrumentsDD"]); // gets the id from the instruments drop down box
            int studentsid = Convert.ToInt32(formCollection["StudentsDD"]); // gets the id from the instruments drop down box
            Student newInstrumentChosen = new Student() { StudentID = studentsid, InstrumentID = newInstrument };

            MusicSchoolDB.Students.Attach(newInstrumentChosen);
            MusicSchoolDB.Entry(newInstrumentChosen).Property(x => x.InstrumentID).IsModified = true;
            MusicSchoolDB.SaveChanges();
            
            IEnumerable<Student> students = MusicSchoolDB.Students.ToList(); // gets a list of students
            return RedirectToAction("Enrolment", students);
        }

        /* Page that shows all the students with their respective tutors */
        public ActionResult Classes()
        {
            IEnumerable<Tutor> allTutors = MusicSchoolDB.Tutors.ToList();
            return View("Classes", allTutors);
        }

        /* Assigning students to a particular tutor */
        public ActionResult AssignTutors()
        {
            int chosenTutor = 0;
            IEnumerable<Student> allStudents = MusicSchoolDB.Students.ToList(); // Get a list of students
            IEnumerable<InstrumentQualification> allQualifications = MusicSchoolDB.InstrumentQualifications.ToList(); // Get a list of qualifications
            foreach (Student s in allStudents) // Go through the list of students
            {                
                foreach (InstrumentQualification iq in allQualifications)
                {
                    if (s.InstrumentID == iq.InstrumentID) // Checks whether the students instrument is the same as the tutors instrument
                    {
                        if (iq.Level >= 6) // Tutor level must be at least 6 in order to teach the instrument
                        {
                            if (s.Age >= 16)
                            {
                                if (iq.Level >= 8)
                                {
                                    chosenTutor = iq.TutorID; // Assigns tutor id if the students is older than 16 and the tutor has a good enough level 
                                }
                            }
                            else
                            {
                                chosenTutor = iq.TutorID; // Assigns tutor id to a regular student
                            }
                        }
                    }
                }
                /* Adds the tutor id into the database for students */
                Student newTutor = new Student() { StudentID = s.StudentID, TutorID = chosenTutor };
                using (MusicSchoolDbContext db = new MusicSchoolDbContext())
                {
                    db.Students.Attach(newTutor);
                    db.Entry(newTutor).Property(x => x.TutorID).IsModified = true;
                    db.SaveChanges();
                }                
            }
            return View();
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
	}
}