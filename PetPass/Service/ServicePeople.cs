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
	internal class ServicePeople : IServicePeople
	{
		private readonly HttpClient _httpClient;
		string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMCIsIm5iZiI6MTY5NzcxNDg2NiwiZXhwIjoxNjk3NzI5MjY1LCJpYXQiOjE2OTc3MTQ4NjZ9.kdNuYYdSzGKltys8RODoXb9rmTNZSJycwnyWZp0Y_cA";

		public ServicePeople()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:44313/");
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
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
	}
}
