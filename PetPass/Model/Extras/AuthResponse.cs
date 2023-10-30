using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model.Extras
{
    public class AuthResponse
    {
        public int userID;
        public string Token;
        public bool FirstLogin;
        public char Role;
		public string? Photo;
	}
}
