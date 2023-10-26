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
	public class PersonService : BaseService, IPersonService
	{

		public PersonService() : base()
		{

		}

		public async Task<PersonRegister> CreatePersonAsync(PersonRegister person, string authToken)
		{
			try
			{
				if (person == null)
				{
					Console.WriteLine("El objeto 'person' es nulo.");
					return null;
				}

				string personJson = JsonConvert.SerializeObject(person);
				var content = new StringContent(personJson, Encoding.UTF8, "application/json");

				// Agrega el token de autorización al encabezado de la solicitud
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

				HttpResponseMessage response = await _httpClient.PostAsync("PetPass/People/CreateBrigadier", content);

				// Limpia el encabezado de autorización para futuras solicitudes
				_httpClient.DefaultRequestHeaders.Authorization = null;

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrEmpty(responseBody))
					{
						Console.WriteLine("La respuesta del servidor está vacía.");
						return null;
					}

					PersonRegister createdPerson = JsonConvert.DeserializeObject<PersonRegister>(responseBody);
					return createdPerson;
				}
				else
				{
					Console.WriteLine($"Error al crear la persona. Código de estado HTTP: {response.StatusCode}");
					return null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al crear la persona: {ex.Message}");
				return null;
			}
		}


		public async Task<List<PersonRegister>> GetPeopleAsync(string token)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					// Define la URL del servicio
					string apiUrl = "https://localhost:44313/PetPass/People";

					// Configura el encabezado de autorización con el token
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					// Realiza una solicitud GET al servicio
					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						// Lee y deserializa el contenido de la respuesta a una lista de Persons
						string responseContent = await response.Content.ReadAsStringAsync();
						List<PersonRegister> peopleList = JsonConvert.DeserializeObject<List<PersonRegister>>(responseContent);
						return peopleList;
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

		public async Task<bool> UpdatePersonAsync(string token, PersonRegister person)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					// Define la URL del servicio
					string apiUrl = "https://localhost:44313/PetPass/People/UpdatePerson";

					// Configura el encabezado de autorización con el token
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					// Serializa el objeto Person a JSON
					string personJson = JsonConvert.SerializeObject(person);
					var content = new StringContent(personJson, Encoding.UTF8, "application/json");

					// Realiza una solicitud PUT al servicio
					HttpResponseMessage response = await client.PutAsync(apiUrl, content);

					if (response.IsSuccessStatusCode)
					{
						// La actualización fue exitosa
						return true;
					}
					else
					{
						// Maneja el error si la solicitud no fue exitosa
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				// Maneja las excepciones, como problemas de red
				return false;
			}
		}

		public async Task<PersonRegister> GetPersonDetailsAsync(string token, int personId)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					// Define la URL del servicio con el ID de la persona
					string apiUrl = $"https://localhost:44313/PetPass/People/{personId}";

					// Configura el encabezado de autorización con el token
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					// Realiza una solicitud GET al servicio
					HttpResponseMessage response = await client.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						// Lee y deserializa el contenido de la respuesta a un objeto Person
						string responseContent = await response.Content.ReadAsStringAsync();
						PersonRegister person = JsonConvert.DeserializeObject<PersonRegister>(responseContent);
						return person;
					}
					else
					{
						// Maneja el error si la solicitud no fue exitosa
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				// Maneja las excepciones, como problemas de red
				return null;
			}
		}

		public async Task<bool> DeletePersonAsync(string token, int personId)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					// Define la URL del servicio con el ID de la persona
					string apiUrl = $"https://localhost:44313/PetPass/People/DeletePerson?id={personId}";

					// Configura el encabezado de autorización con el token
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					// Realiza una solicitud POST al servicio
					HttpResponseMessage response = await client.PostAsync(apiUrl, null);

					if (response.IsSuccessStatusCode)
					{
						// La eliminación fue exitosa
						return true;
					}
					else
					{
						// Maneja el error si la solicitud no fue exitosa
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				// Maneja las excepciones, como problemas de red
				return false;
			}
		}
	}
}
