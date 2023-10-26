using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	public interface IPersonService
	{
		Task<PersonRegister> CreatePersonAsync(PersonRegister person, string authToken);
		Task<List<PersonRegister>> GetPeopleAsync(string token);
		Task<bool> UpdatePersonAsync(string token, PersonRegister person);
		Task<PersonRegister> GetPersonDetailsAsync(string token, int personId);
		Task<bool> DeletePersonAsync(string token, int personId);
	}
}
