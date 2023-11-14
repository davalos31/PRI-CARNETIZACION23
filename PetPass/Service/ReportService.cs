using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public class ReportService :  BaseService, IReport
    {

        public ReportService() : base()
        {

        }


 


        public async Task<List<BrigadierData>> GetBrigadierDataAsync(string authToken)
        {
           
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Reports/GetPetsRegisteredByBrigadiers"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<BrigadierData>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar una lista vacía
                        return new List<BrigadierData>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return new List<BrigadierData>();
            }
        }

        public async Task<List<BrigadierData>> GetZoneReport(string authToken)
        {
           

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Reports/GetZoneReport"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<BrigadierData>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar una lista vacía
                        return new List<BrigadierData>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return new List<BrigadierData>();
            }
        }

        public async Task<List<BrigadierData>> GetZoneAndTotalDogs(string token)
        {

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "PetPass/Reports/GetZoneAndTotalDogs"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var campaigns = JsonConvert.DeserializeObject<List<BrigadierData>>(responseContent);
                        return campaigns;
                    }
                    else
                    {
                        // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar una lista vacía
                        return new List<BrigadierData>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
                return new List<BrigadierData>();
            }
        }


    }

 }

