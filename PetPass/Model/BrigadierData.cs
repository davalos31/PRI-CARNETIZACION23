using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class BrigadierData
    {
        public string BrigadierName { get; set; }
        public int TotalPetsRegistered { get; set; }

        public string ZoneName { get; set; }
        public int TotalBrigadiers { get; set; }

        public int TotalDogs {  get; set; }
    }
}
