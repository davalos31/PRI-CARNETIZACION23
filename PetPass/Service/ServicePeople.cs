using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetPass.Service
{
	internal class ServicePeople : BaseService, IServicePeople
	{

		public ServicePeople() : base()
		{

		}

		public async Task<bool> CreateOwner(Person person, int userId)
		{
			try
			{
				if (person == null) return false;
				if (userId <= 0) return false;

				string personJson = JsonConvert.SerializeObject(person);
				var content = new StringContent(personJson, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PostAsync($"PetPass/People/CreateOwner?userId={userId}", content);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public async Task<Person> findbyCI(string ci)
		{
			try
			{
				if (ci == null) return null;

				HttpResponseMessage response = await _httpClient.GetAsync($"PetPass/People/FindByCI?ci={ci}");

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrEmpty(responseBody))
					{
						Console.WriteLine("La respuesta del servidor está vacía.");
						return null;
					}

					return JsonConvert.DeserializeObject<Person>(responseBody);
				}
				else
				{
					Console.WriteLine($"Error al usar la api. Código de estado HTTP: {response.StatusCode}");
					return null;
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
