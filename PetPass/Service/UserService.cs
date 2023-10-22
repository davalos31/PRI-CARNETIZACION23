using Microsoft.SqlServer;
using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;
        private readonly HttpClient _httpClient; // Agregar la instancia de HttpClient
    
        public UserService(string connectiom)
        {
            _connectionString = connectiom;
            _httpClient = new HttpClient(); // Inicializar HttpClient
        }

        public UserService()
        {
            _httpClient = new HttpClient();
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

        public async Task<AuthToken> GetAuthTokenAsync(string username, string password)
        {
            try
            {
                // URL del punto de autenticación
                string authUrl = "https://localhost:44313/PetPass/Users/Login";  // Reemplaza con la URL correcta

                // Crear un objeto JSON con las credenciales
                var credentials = new
                {
                    Username = username,
                    Password = password
                };

                // Serializar el objeto a JSON
                string jsonCredentials = JsonConvert.SerializeObject(credentials);

                // Crear una solicitud POST con el cuerpo JSON
                var content = new StringContent(jsonCredentials, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al servicio de autenticación
                var response = await _httpClient.PostAsync(authUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar la respuesta del servicio
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var authToken = JsonConvert.DeserializeObject<AuthToken>(responseContent);
                    return authToken;
                }
                else
                {
                    // Manejar el caso en que la autenticación no fue exitosa, por ejemplo, lanzar una excepción o devolver null.
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones, como problemas de red o problemas en el servicio.
                // Puedes registrar el error o lanzar una excepción personalizada.
                throw ex;
            }
        }
    }
}
