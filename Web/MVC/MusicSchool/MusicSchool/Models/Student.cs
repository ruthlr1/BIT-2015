using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchool.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int Age { get; set; }
        public Boolean OwnInstrument { get; set; }

        public int? InstrumentID { get; set; }
        public int? TutorID { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual Instrument Instrument { get; set; }
    }
}