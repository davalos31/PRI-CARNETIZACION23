using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class Patrol1
    {
        public int PatrolId { get; set; }
        public DateTime PatrolDate { get; set; }
        public int PersonId { get; set; }
        public byte ZoneId { get; set; }
        public int CampaignId { get; set; }

        public Campaigns? Campaign { get; set; }
        public User? Person { get; set; }
        public Zone? Zone { get; set; }
    }
}
