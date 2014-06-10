using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceptionDesk.Models;

namespace ReceptionDeskTest.Models
{
    public class StudyMeetingTest
    {
        [TestClass]
        public class 一般
        {
            private StudyMeeting sut;

            [TestInitialize]
            public void SetUp()
            {
                sut = new StudyMeeting()
                {
                    SeminarFee = 500,
                    SocialGatheringFee = 4000,
                    SocialGatheringFeeForStudent = 3000
                };

            }
            
            [TestMethod]
            public void 懇親会に参加しない場合は参加料のみ()
            {
                var participant = new Participant()
                {
                    SocialGathering = false,
                    IsStudent = false,
                    IsInstructor = false,
                    IsStaff = false
                };

                var actual = sut.CalcFee(participant);

                Assert.AreEqual(500, actual.SeminarFee);
                Assert.AreEqual(0, actual.SocialGatheringFee);
            }

            [TestMethod]
            public void 懇親会に参加する場合は参加料と懇親会費()
            {
                var participant = new Participant()
                {
                    SocialGathering = true,
                    IsStudent = false,
                    IsInstructor = false,
                    IsStaff = false
                };

                var actual = sut.CalcFee(participant);

                Assert.AreEqual(500, actual.SeminarFee);
                Assert.AreEqual(4000, actual.SocialGatheringFee);
            }

            [TestMethod]
            public void 講師は参加費無料()
            {
                var participant = new Participant()
                {
                    SocialGathering = true,
                    IsStudent = false,
                    IsInstructor = true,
                    IsStaff = false
                };

                var actual = sut.CalcFee(participant);

                Assert.AreEqual(0, actual.SeminarFee);
                Assert.AreEqual(4000, actual.SocialGatheringFee);
            }

            [TestMethod]
            public void スタッフは参加費無料()
            {
                var participant = new Participant()
                {
                    SocialGathering = true,
                    IsStudent = false,
                    IsInstructor = false,
                    IsStaff = true
                };

                var actual = sut.CalcFee(participant);

                Assert.AreEqual(0, actual.SeminarFee);
                Assert.AreEqual(4000, actual.SocialGatheringFee);
            }
        }

        [TestClass]
        public class 学生
        {
            private StudyMeeting sut;

            [TestInitialize]
            public void SetUp()
            {
                sut = new StudyMeeting()
                {
                    SeminarFee = 500,
                    SocialGatheringFee = 4000,
                    SocialGatheringFeeForStudent = 3000
                };

            }

            [TestMethod]
            public void 学生は参加費無料かつ懇親会費は学生料金()
            {
                var participant = new Participant()
                {
                    SocialGathering = true,
                    IsStudent = true,
                    IsInstructor = false,
                    IsStaff = false
                };

                var actual = sut.CalcFee(participant);

                Assert.AreEqual(0, actual.SeminarFee);
                Assert.AreEqual(3000, actual.SocialGatheringFee);
            }
        }

    }
}
