using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchool.Models
{
    public class Tutor
    {
        public int TutorID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int PhoneNumber { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual List<InstrumentQualification> InstrumentQualifications { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}