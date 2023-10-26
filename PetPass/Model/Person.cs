using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
	public class Person
	{
		public int PersonId { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string? LastName { get; set; }
		public string Ci { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public int Phone { get; set; }
		public string Email { get; set; }
		public short State { get; set; }
		public Person() { }
		public Person(int personId, string name, string firstName, string lastName, string ci, string gender, string address, int phone, string email, short state)
		{
			PersonId = personId;
			Name = name;
			FirstName = firstName;
			LastName = lastName;
			Ci = ci;
			Gender = gender;
			Address = address;
			Phone = phone;
			Email = email;
			State = state;
		}
	}
}
