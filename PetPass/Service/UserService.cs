using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;
        public UserService(string connectiom) 
        {
            _connectionString = connectiom;
        }

        public async Task<string> GetUserRoleAsync(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(
                    "SELECT [Rol] FROM [DbPetPass].[dbo].[User] WHERE [username] = @Username AND [userpassword] = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    string role = await command.ExecuteScalarAsync() as string;
                    return role;
                }
            }
        }

        public async Task<bool> ValidarUsuarioAsync(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT COUNT(*) FROM [User] WHERE [username] = @Username AND [userpassword] = @Password", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)await command.ExecuteScalarAsync();

                        // Si count es mayor que 0, las credenciales son válidas.
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, log, etc.
                return false;
            }
        }

        public async Task<int> GetPersonIDByUsernameAsync(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT [personID] FROM [User] WHERE [username] = @Username", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }

                        return 0; // Retorna 0 si no se encontró un personID.
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la recuperación.
                Console.WriteLine($"Error al obtener el personID: {ex.Message}");
                return 0;
            }
        }
    }
}
