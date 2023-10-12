using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
	internal class Person
	{
		public int PersonId { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CI { get; set; }
		public char Gender { get; set; }
		public string Address { get; set; }
		public int Phone { get; set; }
		public string Email { get; set; }


		public Person(string name, string firstName, string? lastName, string cI, char gender, string address, int phone, string email)
		{
			Name = name;
			FirstName = firstName;
			LastName = lastName;
			CI = cI;
			Gender = gender;
			Address = address;
			Phone = phone;
			Email = email;
		}
	}
}
