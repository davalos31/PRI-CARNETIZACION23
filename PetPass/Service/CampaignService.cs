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
    public class CampaignService : BaseService, ICampaign
    {


        public CampaignService() : base()
        {

        }

        public async Task<Campaigns> CreateCampaignAsync(Campaigns campaign, string authToken)
        {
            try
            {
                var campaignJson = JsonConvert.SerializeObject(campaign);
                var content = new StringContent(campaignJson, System.Text.Encoding.UTF8, "application/json");

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "PetPass/Campaigns/Create"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    requestMessage.Content = content;

                    var response = await _httpClient.SendAsync(requestMessage);

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

        public async Task<List<Campaigns>> GetCampaignsAsync(string authToken)
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Campaigns"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                    var response = await _httpClient.SendAsync(requestMessage);

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
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return new List<Campaigns>();
            }
        }




    }
}
