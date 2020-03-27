using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoResourceRecoveryControlService.Models
{
    public class PointExchange
    {
        public string Id { get; set; }
        public DateTime RecodeDate { get; set; }
        public int PublishedTime { get; set; }
        public string MemberNo { get; set; }
        public Machine Machine { get; set; }
        public int Point { get; set; }



    }
}
