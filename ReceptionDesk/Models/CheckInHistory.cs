using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Generator;

namespace ReceptionDesk.Models
{
    public class CheckInHistory
    {
        public int Id { get; set; }
        public DateTime CheckInTime { get; private set; }
        public virtual Participant Participant { get; set; }

        public CheckInHistory()
        {
            CheckInTime = DateTime.UtcNow;
        }
    }
}