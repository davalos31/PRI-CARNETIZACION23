using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using PetPass.Model;
using PetPass.Model.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    internal class LoginService : BaseService, ILoginService
	{
		public LoginService() : base()
		{

		}

		public async Task<AuthResponse> Login(string Username, string Userpassword)
		{
			try
			{
				if (Username == null || Userpassword == null)
				{
					Console.WriteLine("El ingrese el usuario y contraseña.");
					return null;
				}

				string dataJson = JsonConvert.SerializeObject(new UserRequest(Username,GetSha256(Userpassword)));
				var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PostAsync($"PetPass/Users/Login", content);

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();

					if (string.IsNullOrEmpty(responseBody))
					{
						Console.WriteLine("La respuesta del servidor está vacía.");
						return null;
					}

					return JsonConvert.DeserializeObject<AuthResponse>(responseBody);
				}
				else
				{
					Console.WriteLine($"Error al usar la api. Código de estado HTTP: {response.StatusCode}");
					return null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error general: {ex.Message}");
				return null;
			}
		}

		public async Task<bool> FirstLogin(int userID, string newPassword)
		{
			try
			{
				if (userID <= 0 || newPassword == null)
				{
					Console.WriteLine("error del id y contraseña.");
					return false;
				}

				string dataJson = JsonConvert.SerializeObject(new FirstLoginUser(userID, GetSha256(newPassword)));
				var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PutAsync($"PetPass/Users/firstPassword", content);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				else
				{
					Console.WriteLine($"Error al usar la api. Código de estado HTTP: {response.StatusCode}");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error general: {ex.Message}");
				return false;
			}
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

				string dataJson = JsonConvert.SerializeObject(new AuthRecoveryPassword(userID, GetSha256(codeRecovery), GetSha256(newPassword)));
				var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _httpClient.PutAsync($"PetPass/Users/RecoveryPassword", content);

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

		private string GetSha256(string str)
		{
			SHA256 sha256 = SHA256Managed.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = sha256.ComputeHash(encoding.GetBytes(str));
			for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
			return sb.ToString();
		}
	}
}
