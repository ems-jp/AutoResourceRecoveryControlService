using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoResourceRecoveryControlService.Models
{
    public class AutoScale
    {
        public string Id { get; set; }
        public DateTime RecodeDate { get; set; }
        //public string UID { get; set; }
        public string MemberNo { get; set; }
        public Machine Machine { get; set; }
        public Item Item { get; set; }
        public double Scale { get; set; }
        public int Point { get; set; }
        public string Unit { get; set; }
        public int TotalPoint { get; set; }
        public string Status { get; set; }
        public string WeightCheck { get; set; }
        public string TimeCheck { get; set; }
    }
}
