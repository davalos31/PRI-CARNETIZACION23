using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public interface IUserService
    {
        Task<int> GetPersonIDByUsernameAsync(string username);
        Task<string> GetUserRoleAsync(string userName, string password);
      //  Task LoginAsync(string username, string password);
        Task<bool> ValidarUsuarioAsync(string username, string password);
       // Task<AuthToken> GetAuthTokenAsync(string username, string password);
    }
}
