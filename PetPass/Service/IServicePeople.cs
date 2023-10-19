using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	internal interface IServicePeople
	{
		Task<bool> CreateOwner(Person person,int userId);
	}
}
