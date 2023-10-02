using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class Patrol1
    {
         public int patrolID { get; set; }
        public DateTime patrolDate { get; set; }
        public Person Person { get; set; } // Relación con la tabla Person
        public Zone Zone { get; set; }     // Relación con la tabla Zone
        public Campaign Campaign { get; set; } // Relación con la tabla Campaign
    }
}
