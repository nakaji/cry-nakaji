using System.Web.Http;
using ReceptionDesk.Models;

namespace ReceptionDesk.Controllers.api
{
    /// <summary>
    /// 受付時の集金額計算API
    /// </summary>
    public class FeeController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        /// <summary>
        /// 条件から集金額を計算する
        /// </summary>
        /// <param name="id">勉強会ID</param>
        /// <param name="attendSocialGathering">懇親会参加（true/false）</param>
        /// <param name="isStudent">学生（true/false）</param>
        /// <param name="isInstructor">講師（true/false）</param>
        /// <param name="isConcerned">会場関係者（true/false）</param>
        /// <param name="isStaff">勉強会スタッフ（true/false）</param>
        /// <returns></returns>
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
