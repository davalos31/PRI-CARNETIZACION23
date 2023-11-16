using Newtonsoft.Json;
using PetPass.Model;
using PetPass.Model.Extras;
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
        private readonly HttpClient _httpClient;


        public PatrolService() : base()
        {


            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://petpass-api2023.azurewebsites.net/");
            // Assuming 'session' is an instance variable or parameter of your class
            if (session.AuthResponse != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AuthResponse.Token);
            }
        }

        public async Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol)
        {
            try
            {
                var patrolJson = JsonConvert.SerializeObject(patrol);
                var content = new StringContent(patrolJson, System.Text.Encoding.UTF8, "application/json");

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "PetPass/Patrol/CreatePatrol"))
                {
                    // Add the authorization token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    requestMessage.Content = content;

                    var response = await _httpClient.SendAsync(requestMessage);

                    // Return true if the request is successful (status code 2xx)
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, for example, log the error
                Console.WriteLine("Error: " + ex.Message);

                // Return false in case of an exception
                return false;
            }
        }



        public async Task<Patrol1> GetPatrolDetailsAsyncApi(string token, int patrolId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {



                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    HttpResponseMessage response = await client.GetAsync($"PetPass/Patrol/{patrolId}");

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
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Patrol/GetZones"))
                {
                    // Add the authorization token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", session.AuthResponse.Token);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<Zone>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Handle errors, for example, throw an exception or return an empty list
                        return new List<Zone>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                Console.WriteLine("Error: " + ex.Message);
                return new List<Zone>();
            }




        }

        public async Task<List<PatrolInfo>> GetPatrolAsyncApi(string token)
        {

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Patrol"))
                {
                    // Add the authorization token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", session.AuthResponse.Token);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<PatrolInfo>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Handle errors, for example, throw an exception or return an empty list
                        return new List<PatrolInfo>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                Console.WriteLine("Error: " + ex.Message);
                return new List<PatrolInfo>();
            }
        }




        public async Task<List<Campaigns>> GetCampaignsAsync(string token)
        {


            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Patrol/GetCampaign"))
                {
                    // Add the authorization token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", session.AuthResponse.Token);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<Campaigns>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Handle errors, for example, throw an exception or return an empty list
                        return new List<Campaigns>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error
                Console.WriteLine("Error: " + ex.Message);
                return new List<Campaigns>();
            }

        }

    }
}
