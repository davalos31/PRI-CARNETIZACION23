using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Model
{
    public class LoginResponse
    {
        public int UserId {  get; set; }
        public string token { get; set; }

        public bool FirstLogin { get; set; }
        public string Role { get; set; }


    }
}
