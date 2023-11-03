using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    internal class PetCreated:Pet
    {
        public int UserId { get; set; }
        public List<string>? Images { get; set; }
        public PetCreated(int petId, string name, string specie, string breed, char gender, DateTime birthDate, string specialFeature, short state, int personId, int userId, List<string> images) : base(petId, name, specie, breed, gender, birthDate, specialFeature, state, personId)
        {
            PetId = petId;
            Name = name;
            Specie = specie;
            Breed = breed;
            Gender = gender;
            BirthDate = birthDate;
            SpecialFeature = specialFeature;
            State = state;
            PersonId = personId;
            UserId = userId;
            Images = images;
        }
    }
}
