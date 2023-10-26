using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	public class PatrolService : BaseService, IPatrol
	{
		private readonly string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";

		//public PatrolService(string connectionString)
		//{
		//    _connectionString = connectionString;
		//}

		public PatrolService() : base()
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


		Task<Person> IPatrol.GetPersonByIdAsync(int personId)
		{
			throw new NotImplementedException();
		}

		Task<List<Person>> IPatrol.GetAllPersonsAsync()
		{
			throw new NotImplementedException();
		}

		Task<List<Patrol1>> IPatrol.GetPatrolsAsync()
		{
			throw new NotImplementedException();
		}
	}
}
