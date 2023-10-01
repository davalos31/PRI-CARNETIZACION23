using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	internal interface IRestorePassword
	{
		Task<int> FindByEmail(string email);
		Task<bool> RecoveryPassword(int userID, string codeRecovery, string newPassword);
	}
}
