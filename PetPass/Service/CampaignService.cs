
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
    public class CampaignService : ICampaign
    {
        private readonly HttpClient _httpClient;
        private readonly string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";

        public CampaignService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44313/");
        }

        public async Task<Campaigns> CreateCampaignAsync(Campaigns campaign)
        {
            try
            {
                var campaignJson = JsonConvert.SerializeObject(campaign);
                var content = new StringContent(campaignJson, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("PetPass/Campaigns/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdCampaign = JsonConvert.DeserializeObject<Campaigns>(responseContent);
                    return createdCampaign;
                }
                else
                {
                    // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<Campaigns> GetCampaignAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"PetPass/Campaigns/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var campaign = JsonConvert.DeserializeObject<Campaigns>(responseContent);
                    return campaign;
                }
                else
                {
                    // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<List<Campaigns>> GetCampaignsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("PetPass/Campaigns");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var campaigns = JsonConvert.DeserializeObject<List<Campaigns>>(responseContent);
                    return campaigns;
                }
                else
                {
                    // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar una lista vacía
                    return new List<Campaigns>();
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return new List<Campaigns>();
            }
        }

        public bool UpdateCampaign(int campaignID, string newName, DateTime newStartDate, DateTime newEndDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Campaign " +
                                                           "SET name = @NewName, " +
                                                           "    StartDate = @NewStartDate, " +
                                                           "    EndDate = @NewEndDate " +
                                                           "WHERE campaignID = @CampaignID", connection))
                {
                    // Parámetros
                    command.Parameters.Add(new SqlParameter("@NewName", newName));
                    command.Parameters.Add(new SqlParameter("@NewStartDate", newStartDate));
                    command.Parameters.Add(new SqlParameter("@NewEndDate", newEndDate));
                    //command.Parameters.Add(new SqlParameter("@NewState", newState));
                    command.Parameters.Add(new SqlParameter("@CampaignID", campaignID));

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Devuelve true si se actualizó al menos una fila
                }
            }
        }

        public bool DeleteCampaign(int campaignID)
        {
            int newState = 1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Campaign " +
                                                           "SET state = @Newstate " +
                                                           "WHERE campaignID = @CampaignID", connection))
                {
                    // Parámetros
                  
                    command.Parameters.Add(new SqlParameter("@NewState", newState));
                    command.Parameters.Add(new SqlParameter("@CampaignID", campaignID));
                   

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Devuelve true si se actualizó al menos una fila
                }
            }
        }


    }
}
