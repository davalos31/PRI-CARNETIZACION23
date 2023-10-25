using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model.Extras
{
	internal class FirstLoginUser
	{
		public int userID;

		public string newPassword;

		public FirstLoginUser(int userID, string newPassword)
		{
			this.userID = userID;
			this.newPassword = newPassword;
		}
	}
}
