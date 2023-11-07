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
    public class ReportService :  IReport
    {

        public ReportService() 
        {

        }


 


        public async Task<List<BrigadierData>> GetBrigadierDataAsync(string token)
        {
            // URL del servicio API
            string apiUrl = "https://localhost:44313/PetPass/Reports/GetPetsRegisteredByBrigadiers";

            using (var httpClient = new HttpClient())
            {
                // Establecer el encabezado de autorización con el token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    // Realizar la solicitud GET al servicio API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena JSON
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta en una lista de objetos
                        List<BrigadierData> brigadierDataList = JsonConvert.DeserializeObject<List<BrigadierData>>(responseBody);

                        return brigadierDataList;
                    }
                    else
                    {
                        Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                }
            }

            // Si hay un error, devuelve una lista vacía o maneja el error de acuerdo a tus necesidades
            return new List<BrigadierData>();
        }

        public async Task<List<BrigadierData>> GetZoneReport(string token)
        {
            // URL del servicio API
            string apiUrl = "https://localhost:44313/PetPass/Reports/GetZoneReport";

            using (var httpClient = new HttpClient())
            {
                // Establecer el encabezado de autorización con el token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    // Realizar la solicitud GET al servicio API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena JSON
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta en una lista de objetos
                        List<BrigadierData> brigadierDataList = JsonConvert.DeserializeObject<List<BrigadierData>>(responseBody);

                        return brigadierDataList;
                    }
                    else
                    {
                        Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                }
            }

            // Si hay un error, devuelve una lista vacía o maneja el error de acuerdo a tus necesidades
            return new List<BrigadierData>();
        }

        public async Task<List<BrigadierData>> GetZoneAndTotalDogs(string token)
        {
            // URL del servicio API
            string apiUrl = "https://localhost:44313/PetPass/Reports/GetZoneAndTotalDogs";

            using (var httpClient = new HttpClient())
            {
                // Establecer el encabezado de autorización con el token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    // Realizar la solicitud GET al servicio API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena JSON
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta en una lista de objetos
                        List<BrigadierData> brigadierDataList = JsonConvert.DeserializeObject<List<BrigadierData>>(responseBody);

                        return brigadierDataList;
                    }
                    else
                    {
                        Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                }
            }

            // Si hay un error, devuelve una lista vacía o maneja el error de acuerdo a tus necesidades
            return new List<BrigadierData>();
        }


    }

 }

