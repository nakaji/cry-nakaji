using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ReceptionDesk.Models
{
    public class Participant
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "アカウント名")]
        public string Name { get; set; }

        [Display(Name = "懇親会")]
        public bool SocialGathering { get; set; }

        [Display(Name = "未成年")]
        public bool IsMinors { get; set; }

        [Display(Name = "学生")]
        public bool IsStudent { get; set; }

        [Display(Name = "講師")]
        public bool IsInstructor { get; set; }
         
        [Display(Name = "スタッフ")]
        public bool IsStaff { get; set; }

        [Display(Name = "アイコン画像")]
        public string ImageUrl { get; set; }

        [Display(Name = "チェックイン")]
        public bool CheckedIn { get; set; }

        [Required]
        public virtual StudyMeeting StudyMeeting { get; set; }

        public FeeInfo CalcFee()
        {
            return StudyMeeting.CalcFee(this);
        }
    }
}