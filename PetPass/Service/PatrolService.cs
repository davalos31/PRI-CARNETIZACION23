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
		

		public PatrolService() : base()
		{

		}

		public async Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol)
		{
			//try
			//{
			//	using (HttpClient client = new HttpClient())
			//	{

			//		string patrolJson = JsonConvert.SerializeObject(patrol);

					
			//		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					
			//		var content = new StringContent(patrolJson, Encoding.UTF8, "application/json");

					
			//		HttpResponseMessage response = await client.PostAsync("PetPass/Patrol/CreatePatrol", content);

			//		if (response.IsSuccessStatusCode)
			//		{
						
			//			return true;
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{
			
			//	Console.WriteLine($"Error al crear el Patrol: {ex.Message}");
			//}

	
			//return false;



            try
            {
                var campaignJson = JsonConvert.SerializeObject(patrol);
                var content = new StringContent(campaignJson, System.Text.Encoding.UTF8, "application/json");

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "PetPass/Patrol/CreatePatrol"))
                {
                    // Agregar el token de autorización a la solicitud
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    requestMessage.Content = content;

                    var response = await _httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var createdCampaign = JsonConvert.DeserializeObject<Patrol1>(responseContent);
                        return true;
                    }
                    else
                    {
                        // Maneja los errores, por ejemplo, puedes lanzar una excepción o retornar null
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, por ejemplo, registra el error
                Console.WriteLine("Error: " + ex.Message);
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
            //try
            //{
            //	using (HttpClient client = new HttpClient())
            //	{


            //		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //		HttpResponseMessage response = await client.GetAsync("PetPass/Patrol/GetZones");

            //		if (response.IsSuccessStatusCode)
            //		{

            //			string responseContent = await response.Content.ReadAsStringAsync();
            //			List<Zone> zoneList = JsonConvert.DeserializeObject<List<Zone>>(responseContent);
            //			return zoneList;
            //		}
            //		else
            //		{

            //			Console.WriteLine("Error: " + response.StatusCode);
            //			return null;
            //		}
            //	}
            //}
            //catch (Exception ex)
            //{

            //	Console.WriteLine("Error: " + ex.Message);
            //	return null;
            //}

            string apiUrl = "https://localhost:44313/PetPass/Patrol/GetZones";

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
                        List<Zone> brigadierDataList = JsonConvert.DeserializeObject<List<Zone>>(responseBody);

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
            return new List<Zone>();


        }

		public async Task<List<PatrolInfo>> GetPatrolAsyncApi(string token)
		{
            //try
            //{
            //	using (HttpClient client = new HttpClient())
            //	{


            //		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //		HttpResponseMessage response = await client.GetAsync("PetPass/Patrol");

            //		if (response.IsSuccessStatusCode)
            //		{

            //			string responseContent = await response.Content.ReadAsStringAsync();
            //			List<Patrol1> patrolList = JsonConvert.DeserializeObject<List<Patrol1>>(responseContent);
            //			return patrolList;
            //		}
            //		else
            //		{

            //			Console.WriteLine("Error: " + response.StatusCode);
            //			return null;
            //		}
            //	}
            //}
            //catch (Exception ex)
            //{

            //	Console.WriteLine("Error: " + ex.Message);
            //	return null;
            //}


            string apiUrl = "https://localhost:44313/PetPass/Patrol";

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
                        List<PatrolInfo> brigadierDataList = JsonConvert.DeserializeObject<List<PatrolInfo>>(responseBody);

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
            return new List<PatrolInfo>();
        }




		public async Task<List<Campaigns>> GetCampaignsAsync(string token)
		{


            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {


            //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //        HttpResponseMessage response = await client.GetAsync("PetPass/Patrol/GetCampaign");

            //        if (response.IsSuccessStatusCode)
            //        {

            //            string responseContent = await response.Content.ReadAsStringAsync();
            //            List<Campaigns> zoneList = JsonConvert.DeserializeObject<List<Campaigns>>(responseContent);
            //            return zoneList;
            //        }
            //        else
            //        {

            //            Console.WriteLine("Error: " + response.StatusCode);
            //            return null;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine("Error: " + ex.Message);
            //    return null;
            //}
            string apiUrl = "https://localhost:44313/PetPass/Patrol/GetCampaign";

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
                        List<Campaigns> brigadierDataList = JsonConvert.DeserializeObject<List<Campaigns>>(responseBody);

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
            return new List<Campaigns>();

        }

		

		
	}
}
