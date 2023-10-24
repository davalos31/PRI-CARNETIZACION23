using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	internal class LoginService : ILoginService
	{
		private readonly HttpClient _httpClient;

		public LoginService()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:44313/");
			//token
		}

		public async void FirstLogin(int userID, string newPassword)
		{
			throw new NotImplementedException();
		}


		public async Task<int> FindByEmail(string email)
		{
			try
			{
				if (email == null)
				{
					Console.WriteLine("El email es nulo.");
					return 0;
				}

				HttpResponseMessage response = await _httpClient.PutAsync($"PetPass/Users/FindByEmail?email={email}", null);

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrEmpty(responseBody))
					{
						Console.WriteLine("La respuesta del servidor está vacía.");
						return 0;
					}

					return JsonConvert.DeserializeObject<int>(responseBody);
				}
				else
				{
					Console.WriteLine($"Error al usar la api. Código de estado HTTP: {response.StatusCode}");
					return 0;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error general: {ex.Message}");
				return 0;
			}
		}

		public async Task<bool> RecoveryPassword(int userID, string codeRecovery, string newPassword)
		{
			try
			{
				if (userID == 0)
				{
					Console.WriteLine("El email es nulo.");
					return false;
				}
				if (string.IsNullOrEmpty(codeRecovery))
				{
					Console.WriteLine("El codigo de recuperacion es nulo.");
					return false;
				}
				if (string.IsNullOrEmpty(newPassword))
				{
					Console.WriteLine("La contraseña es nula.");
					return false;
				}

				HttpResponseMessage response = await _httpClient.PutAsync($"PetPass/Users/RecoveryPassword?userID={userID}&codeRecovery={codeRecovery}&newPassword={newPassword}", null);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					Console.WriteLine($"Error al actualizar la contraseña. Código de estado HTTP: {response.StatusCode}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al actualizar la contraseña: {ex.Message}");
				return false;
			}
		}
	}
}
