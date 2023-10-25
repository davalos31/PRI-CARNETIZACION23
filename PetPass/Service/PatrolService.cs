using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace PetPass.Service
{
    public class PatrolService : IPatrol
    {
        private readonly string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";

        //public PatrolService(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public PatrolService()
        {

        }




       

        public async Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define la URL del servicio API
                    string apiUrl = "https://localhost:44313/PetPass/Patrol/CreatePatrol";

                    // Serializa el objeto Patrol a JSON
                    string patrolJson = JsonConvert.SerializeObject(patrol);

                    // Configura el encabezado de autorización con el token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Crea un contenido de cadena con el JSON
                    var content = new StringContent(patrolJson, Encoding.UTF8, "application/json");

                    // Realiza una solicitud POST al servicio
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Si la solicitud es exitosa, devuelve true
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante la solicitud
                Console.WriteLine($"Error al crear el Patrol: {ex.Message}");
            }

            // Si algo salió mal, devuelve false
            return false;
        }



        public async Task<Patrol1> GetPatrolDetailsAsyncApi(string token, int patrolId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define la URL del servicio
                    string apiUrl = $"https://localhost:44313/PetPass/Patrol/{patrolId}"; // Asegúrate de usar la URL correcta.

                    // Configura el encabezado de autorización con el token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Realiza una solicitud GET al servicio
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Lee y deserializa el contenido de la respuesta a un objeto Patrol
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Patrol1 patrol = JsonConvert.DeserializeObject<Patrol1>(responseContent);
                        return patrol;
                    }
                    else
                    {
                        // Maneja el error si la solicitud no fue exitosa
                        Console.WriteLine("Error: " + response.StatusCode);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones, como problemas de red
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Zone>> GetZonesAsyncApi(string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define la URL del servicio
                    string apiUrl = "https://localhost:44313/PetPass/Patrol/GetZones";

                    // Configura el encabezado de autorización con el token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Realiza una solicitud GET al servicio
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Lee y deserializa el contenido de la respuesta a una lista de Persons
                        string responseContent = await response.Content.ReadAsStringAsync();
                        List<Zone> zoneList = JsonConvert.DeserializeObject<List<Zone>>(responseContent);
                        return zoneList;
                    }
                    else
                    {
                        // Maneja el error si la solicitud no fue exitosa
                        Console.WriteLine("Error: " + response.StatusCode);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones, como problemas de red
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Patrol1>> GetPatrolAsyncApi(string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define la URL del servicio
                    string apiUrl = "https://localhost:44313/PetPass/Patrol";

                    // Configura el encabezado de autorización con el token
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Realiza una solicitud GET al servicio
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Lee y deserializa el contenido de la respuesta a una lista de Persons
                        string responseContent = await response.Content.ReadAsStringAsync();
                        List<Patrol1> patrolList = JsonConvert.DeserializeObject<List<Patrol1>>(responseContent);
                        return patrolList;
                    }
                    else
                    {
                        // Maneja el error si la solicitud no fue exitosa
                        Console.WriteLine("Error: " + response.StatusCode);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones, como problemas de red
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }










        public async Task<Patrol1> CreatePatrolAsync(Patrol1 patrol)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "INSERT INTO [Patrol] ([patrolDate], [personID], [zoneID], [campaignID]) VALUES (@PatrolDate, @PersonID, @ZoneID, @CampaignID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       // command.Parameters.AddWithValue("@PatrolDate", patrol.patrolDate);
                     //   command.Parameters.AddWithValue("@PersonID", patrol.Person.PersonId);
                        command.Parameters.AddWithValue("@ZoneID", patrol.Zone.ZoneID);
                        command.Parameters.AddWithValue("@CampaignID", patrol.Campaign.CampaignID);

                        command.ExecuteNonQuery();
                    }
                    return patrol;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al insertar la patrulla: {ex.Message}");
                    throw; // Puedes manejar la excepción o lanzarla nuevamente según tus necesidades
                }
            }
        }

        //public async Task<List<Zone>> GetZonesAsync()
        //{
        //    List<Zone> zones = new List<Zone>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sql = "SELECT [ZoneID], [Name] FROM [Zone]";

        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                while (reader.Read())
        //                {
        //                    Zone zone = new Zone
        //                    {
        //                        ZoneID = (int)reader.GetByte(0),
        //                        Name = reader.GetString(1)
        //                    };
        //                    zones.Add(zone);
        //                }
        //            }
        //        }
        //    }

        //    return zones;
        //}

        public async Task<List<Campaigns>> GetCampaignsAsync()
        {
            List<Campaigns> campaigns = new List<Campaigns>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [CampaignID], [Name] FROM [Campaign]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Campaigns campaign = new Campaigns
                            {
                                CampaignID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            campaigns.Add(campaign);
                        }
                    }
                }
            }

            return campaigns;
        }

        public async Task<List<Persons>> GetAllPersonsAsync()
        {
            List<Persons> persons = new List<Persons>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [PersonID], [Name] FROM [Person]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            int personID = reader.GetInt32(0);
                            string personName = reader.GetString(1);

                            Persons person = new Persons
                            {
                                PersonId = personID,
                                Name = personName
                            };

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }


        public async Task<List<Patrol1>> GetPatrolsAsync()
        {
            List<Patrol1> patrols = new List<Patrol1>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [patrolID], [patrolDate], [personID], [zoneID], [campaignID] FROM [Patrol]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read()) // Verificar si hay datos antes de intentar leer
                        {
                            int patrolID = reader.GetInt32(0);
                            DateTime patrolDate = reader.GetDateTime(1);
                            int personID = reader.GetInt32(2);
                            int zoneID = (int)reader.GetByte(3);
                            int campaignID = reader.GetInt32(4);

                            // Obtener los objetos completos de Person, Zone y Campaign utilizando sus respectivos métodos de consulta por ID.
                            Persons person = await GetPersonByIdAsync(personID);
                            Zone zone = await GetZoneByIdAsync(zoneID);
                            Campaigns campaign = await GetCampaignByIdAsync(campaignID);

                            // Crear una instancia de Patrol1 con los objetos completos
                            Patrol1 patrol = new Patrol1
                            {
                                //patrolID = patrolID,
                              //  patrolDate = patrolDate,
                               // Person = person,
                                Zone = zone,
                                Campaign = campaign
                            };

                            patrols.Add(patrol);
                        }
                    }
                }
            }

            return patrols;
        }


        public async Task<Persons> GetPersonByIdAsync(int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [PersonID], [Name] FROM [Person] WHERE [PersonID] = @PersonID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Persons person = new Persons
                            {
                                PersonId = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            return person;
                        }
                    }
                }
            }

            return null; // Retornar null si no se encontró ninguna persona con ese ID
        }

        public async Task<Zone> GetZoneByIdAsync(int zoneId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [ZoneID], [Name] FROM [Zone] WHERE [ZoneID] = @ZoneID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ZoneID", zoneId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Zone zone = new Zone
                            {
                                ZoneID = (int)reader.GetByte(0),
                                Name = reader.GetString(1)
                            };
                            return zone;
                        }
                    }
                }
            }

            return null; // Retornar null si no se encontró ninguna zona con ese ID
        }

        public async Task<Campaigns> GetCampaignByIdAsync(int campaignId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [CampaignID], [Name] FROM [Campaign] WHERE [CampaignID] = @CampaignID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CampaignID", campaignId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            Campaigns campaign = new Campaigns
                            {
                                CampaignID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            return campaign;
                        }
                    }
                }
            }

            return null; // Retornar null si no se encontró ninguna campaña con ese ID
        }

        public async Task UpdatePatrolAsync(Patrol1 updatedPatrol)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "UPDATE [Patrol] SET [patrolDate] = @PatrolDate, [personID] = @PersonID, [zoneID] = @ZoneID, [campaignID] = @CampaignID WHERE [patrolID] = @PatrolID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                     //   command.Parameters.AddWithValue("@PatrolID", updatedPatrol.patrolID);
                       // command.Parameters.AddWithValue("@PatrolDate", updatedPatrol.patrolDate);
                      //  command.Parameters.AddWithValue("@PersonID", updatedPatrol.Person.PersonId);
                        command.Parameters.AddWithValue("@ZoneID", updatedPatrol.Zone.ZoneID);
                        command.Parameters.AddWithValue("@CampaignID", updatedPatrol.Campaign.CampaignID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar la patrulla: {ex.Message}");
                    throw; // Puedes manejar la excepción o lanzarla nuevamente según tus necesidades
                }
            }
        }

        public async Task<List<Reports>> GetPatrolsByZoneAsync()
        {
            string apiUrl = "https://localhost:44313/PetPass/People/ReportZone"; // Reemplaza con la URL real de tu API ReportZone
            List<Reports> patrolsByZone = new List<Reports>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza una solicitud GET a la API ReportZone
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verifica si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta como una cadena JSON
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        // Deserializa la cadena JSON en una lista de objetos Patrol1
                        patrolsByZone = JsonConvert.DeserializeObject<List<Reports>>(jsonContent);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw; // Puedes manejar la excepción o lanzarla nuevamente según tus necesidades
                }
            }

            return patrolsByZone;
        }

        public async Task<List<Reports>> GetReportByPet()
        {
            string apiUrl = "https://localhost:44313/PetPass/People/ReportPet"; // Reemplaza con la URL real de tu API ReportZone
            List<Reports> patrolsByZone = new List<Reports>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza una solicitud GET a la API ReportZone
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verifica si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta como una cadena JSON
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        // Deserializa la cadena JSON en una lista de objetos Patrol1
                        patrolsByZone = JsonConvert.DeserializeObject<List<Reports>>(jsonContent);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw; // Puedes manejar la excepción o lanzarla nuevamente según tus necesidades
                }
            }

            return patrolsByZone;
        }


        public async Task<Patrol1> UpdatePatrolApi(Patrol1 patrol)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE [DbPetPass].[dbo].[Patrol] " +
                                     "SET [patrolDate] = @PatrolDate, " +
                                     "[personID] = @PersonID, " +
                                     "[zoneID] = @ZoneID, " +
                                     "[campaignID] = @CampaignID " +
                                     "WHERE [patrolID] = @PatrolID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatrolDate", patrol.PatrolDate);
                    command.Parameters.AddWithValue("@PersonID", patrol.Person.PersonId);
                    command.Parameters.AddWithValue("@ZoneID", patrol.Zone.ZoneID);
                    command.Parameters.AddWithValue("@CampaignID", patrol.Campaign.CampaignID);
                    command.Parameters.AddWithValue("@PatrolID", patrol.PatrolId);

                    command.ExecuteNonQuery();
                }
            }

            // Debes devolver un valor del tipo Task<Patrol1>, por ejemplo, el mismo objeto patrol
            return patrol;
        }

    }


}

