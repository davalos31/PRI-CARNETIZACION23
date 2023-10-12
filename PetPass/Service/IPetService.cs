using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    internal interface IPetService
    {
        Task<bool> CreatePet(Pet pet);
    }
}
