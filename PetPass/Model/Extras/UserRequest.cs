using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model.Extras
{
	internal class UserRequest
	{
		public string Username;

		public string Userpassword;

		public UserRequest(string username, string userpassword)
		{
			Username = username;
			Userpassword = userpassword;
		}
	}
}
