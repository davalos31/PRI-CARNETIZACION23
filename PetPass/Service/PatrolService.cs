using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                        command.Parameters.AddWithValue("@PatrolDate", patrol.patrolDate);
                        command.Parameters.AddWithValue("@PersonID", patrol.Person.PersonId);
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

        public async Task<List<Zone>> GetZonesAsync()
        {
            List<Zone> zones = new List<Zone>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT [ZoneID], [Name] FROM [Zone]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Zone zone = new Zone
                            {
                                ZoneID = (int)reader.GetByte(0),
                                Name = reader.GetString(1)
                            };
                            zones.Add(zone);
                        }
                    }
                }
            }

            return zones;
        }

        public async Task<List<Campaign>> GetCampaignsAsync()
        {
            List<Campaign> campaigns = new List<Campaign>();

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
                            Campaign campaign = new Campaign
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

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            List<Person> persons = new List<Person>();

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

                            Person person = new Person
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
                            Person person = await GetPersonByIdAsync(personID);
                            Zone zone = await GetZoneByIdAsync(zoneID);
                            Campaign campaign = await GetCampaignByIdAsync(campaignID);

                            // Crear una instancia de Patrol1 con los objetos completos
                            Patrol1 patrol = new Patrol1
                            {
                                patrolID = patrolID,
                                patrolDate = patrolDate,
                                Person = person,
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


        public async Task<Person> GetPersonByIdAsync(int personId)
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
                            Person person = new Person
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

        public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
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
                            Campaign campaign = new Campaign
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
                        command.Parameters.AddWithValue("@PatrolID", updatedPatrol.patrolID);
                        command.Parameters.AddWithValue("@PatrolDate", updatedPatrol.patrolDate);
                        command.Parameters.AddWithValue("@PersonID", updatedPatrol.Person.PersonId);
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

    }


}

