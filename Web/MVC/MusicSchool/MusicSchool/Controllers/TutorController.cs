using MusicSchool.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicSchool.Controllers
{
    public class TutorController : Controller
    {
        private MusicSchoolDbContext MusicSchoolDB = new MusicSchoolDbContext();
        [HttpGet]
        public ActionResult ShowTutors()
        {
            IEnumerable<Tutor> allTutors = MusicSchoolDB.Tutors.ToList();
            InstrumentsDropDownBox();
            return View(allTutors);
        }
        [HttpPost]
        public ActionResult ShowTutors(FormCollection formCollection, string submit)
        {
            IEnumerable<Tutor> tutors = MusicSchoolDB.Tutors.ToList(); // gets a list of students
            try
            {
                switch (submit) // checks which button is clicked
                {
                    case "Save": // adding a tutor
                        Tutor tutor = new Tutor();
                        tutor.FirstName = formCollection["firstName"];
                        tutor.LastName = formCollection["lastName"];
                        tutor.PhoneNumber = Convert.ToInt32(formCollection["PhoneNumber"]);
                        MusicSchoolDB.Tutors.Add(tutor);
                        MusicSchoolDB.SaveChanges();
                        break;
                    case "Update Tutor": // adding a qualification for a particular tutor

                        int tutorIDUpdate = Convert.ToInt32(formCollection["TutorsDD"]);
                        int InstrumentUpdate = Convert.ToInt32(formCollection["AllInstrumentsDD"]);
                        int LevelUpdate = Convert.ToInt32(formCollection["LevelDD"]);
                        InstrumentQualification newQualification = new InstrumentQualification() { TutorID = tutorIDUpdate, InstrumentID = InstrumentUpdate, Level = LevelUpdate };
                        MusicSchoolDB.InstrumentQualifications.Add(newQualification);
                        MusicSchoolDB.SaveChanges();
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return RedirectToAction("ShowTutors", tutors);
        }

        /* Creates a drop down box for instruments*/
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