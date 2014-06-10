using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceptionDesk.Models
{
    public class FeeInfo
    {
        public int SeminarFee { get; set; }
        public int SocialGatheringFee { get; set; }

        public int Toal
        {
            get { return SeminarFee + SocialGatheringFee; }
        }
    }
}