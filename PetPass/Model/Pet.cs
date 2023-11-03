using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
	public class Pet 
	{
		public int PetId { get; set; }
		public string Name { get; set; }
		public string Specie { get; set; }
		public string Breed { get; set; }
		public char Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public string SpecialFeature { get; set; }
		public short State { get; set; }
		public int PersonId { get; set; }


		public Pet(int PetID, string name, string specie, string breed, char gender, DateTime birthDate, string especialFeacture, short state, int personID)
		{
			PetId = PetID;
			Name = name;
			Specie = specie;
			Breed = breed;
			Gender = gender;
			BirthDate = birthDate;
			SpecialFeature = especialFeacture;
			State = state;
			PersonId = personID;
		}
	}
}
