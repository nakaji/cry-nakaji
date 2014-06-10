using System.Collections.Generic;
using ReceptionDesk.Models;

namespace ReceptionDesk.ViewModels
{
    public class ParticipantEditViewModel
    {
        public int StudyMeetingId { get; set; }  
        public Participant Participant { get; set; }
    }

    public class ParticipantAddViewModel
    {
        public StudyMeeting StudyMeeting { get; set; }
        public Participant Participant { get; set; }
        public List<Participant> Participants { get; set; }
    }

}