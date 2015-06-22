using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchool.Models
{
    public class InstrumentQualification
    {
        public int InstrumentQualificationID { get; set; }
        public int? Level { get; set; }
        public virtual int TutorID { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual int InstrumentID { get; set; }
        public virtual Instrument Instrument { get; set; }
    }
}