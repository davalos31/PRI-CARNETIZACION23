using PetPass.Model.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	public class BaseService
	{
		protected readonly HttpClient _httpClient;

		public BaseService()
		{
			_httpClient = new HttpClient();
			//_httpClient.BaseAddress = new Uri("http://www.petpass.somee.com/");
			_httpClient.BaseAddress = new Uri("https://petpass-api2023.azurewebsites.net/");
			if (session.AuthResponse != null)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AuthResponse.Token);
			}
		}
	}
}
