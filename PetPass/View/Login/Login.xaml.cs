using Newtonsoft.Json;
using PetPass.Model;
using PetPass.Service;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.View.Login
{
    public partial class Login : ContentPage
    {
        
       

        public Login()
        {
            InitializeComponent();
           
        }

        private async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                string baseUrl = "https://localhost:44313"; // URL base de tu servicio API
                string apiEndpoint = "/PetPass/Users/Login"; // Ruta al endpoint de inicio de sesi�n
                string fullUrl = baseUrl + apiEndpoint;

                using (HttpClient httpClient = new HttpClient())
                {
                    var userRequest = new User
                    {
                        Username = username,
                        UserPassword = password
                    };

                    string jsonRequest = JsonConvert.SerializeObject(userRequest);
                    StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(fullUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                        if (responseObject != null)
                        {
                            return responseObject; // Devuelve la respuesta completa del inicio de sesi�n
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Inicio de sesi�n fallido. Verifica tus credenciales.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                await DisplayAlert("Error", "Ocurri� un error: " + ex.Message, "OK");
            }

            return null; // Indica que el inicio de sesi�n fall�
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    LoginResponse loginResponse = await LoginAsync(username, password);
                   

                    if (loginResponse != null)
                    {

                        int userId = loginResponse.UserId; // Obt�n el UserID del LoginResponse
                        string tokenValue = loginResponse.token; // Obt�n el token del LoginResponse


                        bool firstLogin = loginResponse.FirstLogin; // Obt�n FirstLogin del LoginResponse


                        string role = loginResponse.Role; // Obt�n Role del LoginResponse
                        // El inicio de sesi�n fue exitoso, muestra el UserID y el token en una alerta
                        string message = $"User ID: {loginResponse.UserId}\nToken: {loginResponse.token}";
                        await DisplayAlert("Informaci�n de inicio de sesi�n", message, "OK");
                        Navigation.PushAsync(new MenuMain(userId,tokenValue));

                        // Realiza otras acciones con el UserID y el token si es necesario
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores en el contexto del evento.
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Usuario y contrase�a son obligatorios", "OK");
            }
        }
    }
}
