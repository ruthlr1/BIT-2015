using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MusicSchool.Models
{
    public class MusicSchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InstrumentQualification> InstrumentQualifications { get; set; }
        
    }
}