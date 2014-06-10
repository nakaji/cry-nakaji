using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReceptionDesk.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext() : base("DefaultConnection") { }

        public DbSet<StudyMeeting> StudyMeetings { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<CheckInHistory> CheckinHistories { get; set; }
    }
}