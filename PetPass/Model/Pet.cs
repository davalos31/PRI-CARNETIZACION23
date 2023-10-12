using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    internal class Pet
    {


        public int PedId { get; set; }
        public string Name { get; set; }
        public string Specie { get; set; }
        public string Breed { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string EspecialFeacture { get; set; }

        public Pet(int pedId, string name, string specie, string breed, char gender, DateTime birthDate, string especialFeacture)
        {
            PedId = pedId;
            Name = name;
            Specie = specie;
            Breed = breed;
            Gender = gender;
            BirthDate = birthDate;
            EspecialFeacture = especialFeacture;
        }


    }
}
