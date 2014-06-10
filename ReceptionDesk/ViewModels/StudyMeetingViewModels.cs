using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReceptionDesk.Models;

namespace ReceptionDesk.ViewModels
{
    public class StudyMeetingTotalFeeViewModels
    {
        public IEnumerable<Participant> Participants { get; set; }
        public int EstSeminarFee { get; set; }
        public int EstSocialGatheringFee { get; set; }
        public int ResSeminarFee { get; set; }
        public int ResSocialGatheringFee { get; set; }
    }
}