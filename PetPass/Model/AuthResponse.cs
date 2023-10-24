using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
	internal class AuthResponse
	{
		public int userID { get; set; }
		public string Token { get; set; }
		public bool FirstLogin { get; set; }
		public char Role { get; set; }
	}
}
