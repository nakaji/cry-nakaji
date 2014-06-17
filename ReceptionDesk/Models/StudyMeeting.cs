using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace ReceptionDesk.Models
{
    public class StudyMeeting
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "勉強会名")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "開催日")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "参加費")]
        public int SeminarFee { get; set; }

        [Display(Name = "懇親会費")]
        public int SocialGatheringFee { get; set; }

        [Display(Name = "懇親会費（学生）")]
        public int SocialGatheringFeeForStudent { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public FeeInfo CalcFee(Participant participant)
        {
            var seminarFee = participant.IsStudent || participant.IsInstructor || participant.IsConcerned ? 0 : SeminarFee;

            var socialGatheringFee = 0;
            if (participant.SocialGathering)
            {
                socialGatheringFee = participant.IsStudent ? SocialGatheringFeeForStudent : SocialGatheringFee;
            }
            return new FeeInfo() { SeminarFee = seminarFee, SocialGatheringFee = socialGatheringFee };
        }
    }
}