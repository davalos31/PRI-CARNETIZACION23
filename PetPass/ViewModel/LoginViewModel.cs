using PetPass.Base;
using PetPass.Service;
using PetPass.View;
using PetPass.View.Patrol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetPass.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private readonly IUserService _userService;


        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new Command(Login);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public ICommand LoginCommand { get; private set; }

        private async void Login()
        {
            // Realiza la lógica de inicio de sesión aquí.
            bool isValidUser = await _userService.ValidarUsuarioAsync(Username, Password);

            if (isValidUser)
            {
                // Obtiene el rol del usuario y redirige según corresponda.
                string userRole = await _userService.GetUserRoleAsync(Username, Password);

                if (userRole == "B")
                {
                    // Redirige a otra página si el rol es "B".
                    // Reemplaza "OtraPagina" con el nombre de la página a la que deseas redirigir.
                    int personID = await GetPersonIDAsync(Username);

                    await Application.Current.MainPage.Navigation.PushAsync(new DetailPatrol());
                    //await Application.Current.MainPage.Navigation.PushAsync(new CreatePatrol(personID));

                    await Application.Current.MainPage.Navigation.PushAsync(new CreatePatrol(personID));
                   


                }
                else
                {
                    // Manejar otros roles o mostrar mensajes de error según sea necesario.
                }

            }
            else
            {
                ErrorMessage = "Nombre de usuario o contraseña incorrectos";
            }
        }

        private async Task<int> GetPersonIDAsync(string username)
        {
            try
            {
                int personID = await _userService.GetPersonIDByUsernameAsync(username);
                return personID;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la recuperación.
                Console.WriteLine($"Error al obtener el personID: {ex.Message}");
                return 0;
            }
        }
    }
}
