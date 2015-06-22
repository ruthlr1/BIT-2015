using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchool.Models
{
    public class Instrument
    {
        public int InstrumentID { get; set; }
        public String Name { get; set; }
        public int? RentalFee { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}