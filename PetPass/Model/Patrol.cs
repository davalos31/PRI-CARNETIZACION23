using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class Patrol
    {
    
       // public int patrolID { get; set; }
        public DateTime patrolDate { get; set; }
        public int personID { get; set; }
        public int zoneID { get; set; }
        public int campaignID { get; set; }
    }
}
