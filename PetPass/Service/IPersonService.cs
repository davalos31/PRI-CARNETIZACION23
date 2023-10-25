using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPass.Model;

namespace PetPass.Service
{
    public interface IPersonService
    {
        Task<Persons> CreatePersonAsync(Persons person, string authToken);
        Task<List<Persons>> GetPeopleAsync(string token);
        Task<bool> UpdatePersonAsync(string token, Persons person);
        Task<Persons> GetPersonDetailsAsync(string token, int personId);
        Task<bool> DeletePersonAsync(string token, int personId);
    }
}

