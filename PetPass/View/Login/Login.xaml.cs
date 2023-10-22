using Newtonsoft.Json;
using PetPass.Service;
using PetPass.ViewModel;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace PetPass.View.Login
{
    public partial class Login : ContentPage
    {
        private readonly string authServiceBaseUrl = "https://localhost:44313/PetPass/Users"; // Reemplaza con la URL de tu microservicio
        private string authToken; // Declarar la variable aqu�

        public Login()
        {
            InitializeComponent();
        }
        private async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                string baseUrl = "https://localhost:44313//PetPass/Users";
                string apiEndpoint = "/Login";
                string fullUrl = baseUrl + apiEndpoint;

                using (HttpClient httpClient = new HttpClient())
                {
                    // Prepara los datos a enviar (username y password)
                    var data = new { username, password };
                    string jsonData = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(fullUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        authToken = jsonResponse; // Asigna el token a la variable authToken
                        return true; // Indica que el inicio de sesi�n fue exitoso
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // El servidor devolvi� un error BadRequest. Puedes mostrar un mensaje personalizado.
                        await DisplayAlert("Error", "Inicio de sesi�n incorrecto. Verifica tus credenciales.", "OK");
                    }
                    else
                    {
                        // Otros errores del servidor o de red
                        await DisplayAlert("Error", "Error al llamar al servicio: " + response.StatusCode, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await DisplayAlert("Error", "Ocurri� un error: " + ex.Message, "OK");
            }

            return false; // Indica que el inicio de sesi�n fall�
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            string username = "asnahue2592";
            string password = "Daxhulk2016";

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    bool loginSuccess = await LoginAsync(username, password);

                    if (loginSuccess)
                    {
                        // El inicio de sesi�n fue exitoso, redirige a otra p�gina
                        await Application.Current.MainPage.Navigation.PushAsync(new CreatePerson());
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
