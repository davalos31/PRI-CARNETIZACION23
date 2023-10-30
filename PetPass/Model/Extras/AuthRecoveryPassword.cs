using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model.Extras
{
	public class AuthRecoveryPassword
	{
		public int UserID { get; set; }
		public string CodeRecovery { get; set; }
		public string newPassword { get; set; }

		public AuthRecoveryPassword(int userID, string codeRecovery, string newPassword)
		{
			UserID = userID;
			CodeRecovery = codeRecovery;
			this.newPassword = newPassword;
		}
	}
}
