using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	internal class ServicePeople : IServicePeople
	{
		private readonly HttpClient _httpClient;

		public ServicePeople()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:44313/");
		}
		public async Task<bool> CreateOwner(Person person)
		{
			try
			{
				if (person == null) return false;

				string personJson = JsonConvert.SerializeObject(person);
				var content = new StringContent(personJson, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PostAsync("PetPass/People/CreateOwner", content);

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
	}
}
