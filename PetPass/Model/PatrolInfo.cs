using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class PatrolInfo
    {

        public int PatrolId { get; set; }
        public DateTime PatrolDate { get; set; }
        public string PersonName { get; set; }
        public string CampaignName { get; set; }
        public string ZoneName { get; set; }
    }
}
