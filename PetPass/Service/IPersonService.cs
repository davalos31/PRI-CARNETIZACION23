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
        Task<Person> CreatePersonAsync(Person person);
        Task<List<Person>> GetPeopleAsync();
    }
}

