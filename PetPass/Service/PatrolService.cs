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

		public PatrolService() : base()
		{

		}

		public async Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
			
					string apiUrl = "https://localhost:44313/PetPass/Patrol/CreatePatrol";

					string patrolJson = JsonConvert.SerializeObject(patrol);

					
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					
					var content = new StringContent(patrolJson, Encoding.UTF8, "application/json");

					
					HttpResponseMessage response = await client.PostAsync(apiUrl, content);

					if (response.IsSuccessStatusCode)
					{
						
						return true;
					}
				}
			}
			catch (Exception ex)
			{
			
				Console.WriteLine($"Error al crear el Patrol: {ex.Message}");
			}

	
			return false;
		}



		public async Task<Patrol1> GetPatrolDetailsAsyncApi(string token, int patrolId)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					
					string apiUrl = $"https://localhost:44313/PetPass/Patrol/{patrolId}"; 

				
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					
					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						
						string responseContent = await response.Content.ReadAsStringAsync();
						Patrol1 patrol = JsonConvert.DeserializeObject<Patrol1>(responseContent);
						return patrol;
					}
					else
					{
					
						Console.WriteLine("Error: " + response.StatusCode);
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				
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
					
					string apiUrl = "https://localhost:44313/PetPass/Patrol/GetZones";

				
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					
					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
					
						string responseContent = await response.Content.ReadAsStringAsync();
						List<Zone> zoneList = JsonConvert.DeserializeObject<List<Zone>>(responseContent);
						return zoneList;
					}
					else
					{
						
						Console.WriteLine("Error: " + response.StatusCode);
						return null;
					}
				}
			}
			catch (Exception ex)
			{
			
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
					
					string apiUrl = "https://localhost:44313/PetPass/Patrol";

					
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					
					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						
						string responseContent = await response.Content.ReadAsStringAsync();
						List<Patrol1> patrolList = JsonConvert.DeserializeObject<List<Patrol1>>(responseContent);
						return patrolList;
					}
					else
					{
					
						Console.WriteLine("Error: " + response.StatusCode);
						return null;
					}
				}
			}
			catch (Exception ex)
			{
			
				Console.WriteLine("Error: " + ex.Message);
				return null;
			}
		}




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

		

		
	}
}
