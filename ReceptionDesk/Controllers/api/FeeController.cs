using System.Web.Http;
using ReceptionDesk.Models;

namespace ReceptionDesk.Controllers.api
{
    public class FeeController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        public FeeInfo Get()
        {
            return null;
        }

        public FeeInfo Get(int id, bool attendSocialGathering, bool isStudent, bool isInstructor, bool isConcerned, bool isStaff)
        {
            var studyMeeting = db.StudyMeetings.Find(id);

            var participant = new Participant()
            {
                SocialGathering = attendSocialGathering,
                IsStudent = isStudent,
                IsInstructor = isInstructor,
                IsConcerned = isConcerned,
                IsStaff = isStaff
            };

            return studyMeeting.CalcFee(participant);
        }
    }
}
