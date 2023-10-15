using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using PetPass.Base;
using PetPass.Resources.Tools;
using PetPass.Model;
using PetPass.Service;
using Command = MvvmHelpers.Commands.Command;
using PetPass.View;
using PetPass.View.Patrol;

namespace PetPass.ViewModel
{
    public class PersonViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IPersonService _personService;

        public PersonViewModel(IPersonService personService)
        {
            _personService = personService;
            CreatePersonCommand = new AsyncCommand(async () => await CreatePerson());
            PagesLogin = new AsyncCommand(async () => await Login());
            SearchCommand = new AsyncCommand(SearchAsync);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ci;
        public string CI
        {
            get => _ci;
            set
            {
                if (_ci != value)
                {
                    _ci = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _phone;
        public int Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _searchCI;
        public string SearchCI
        {
            get => _searchCI;
            set
            {
                if (_searchCI != value)
                {
                    _searchCI = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
        private List<Person> _people;
        public List<Person> People
        {
            get => _people;
            set
            {
                if (_people != value)
                {
                    _people = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CreatePersonCommand { get; set; }

        public ICommand PagesLogin { get; set; }
        public ICommand SearchCommand { get; private set; }

        private async Task SearchAsync()
        {
            try
            {
                List<Person> people = await _personService.GetPeopleAsync();

                if (!string.IsNullOrEmpty(SearchCI))
                {
                    People = people.Where(p => p.CI == SearchCI).ToList();
                }
                else
                {
                    People = people;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la búsqueda: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Error al realizar la búsqueda.", "OK");
            }
        }

        private async Task Login()
        {
            //await App.Current.MainPage.Navigation.PushAsync(new Login());
            await App.Current.MainPage.Navigation.PushAsync(new DetailPatrol());
        }


        private async Task CreatePerson()
        {
            try
            {
                // Agregar registros para verificar que se llama correctamente
                Console.WriteLine("CreatePerson: Creando persona...");

                // Validar que los campos requeridos estén llenos
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(FirstName))
                {
                    Console.WriteLine("CreatePerson: Campos requeridos incompletos.");
                    await Application.Current.MainPage.DisplayAlert("Error", "Nombre y Apellido son obligatorios.", "Aceptar");
                    return;
                }

                // Crear un objeto Person con los datos del formulario
                Person person = new Person
                {
                    Name = this.Name,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    CI = this.CI,
                    Gender = this.Gender,
                    Address = this.Address,
                    Phone = this.Phone,
                    Email = this.Email
                    // Agregar otros campos aquí...
                };

                // Llama al método para crear la persona en el servicio
                Person createdPerson = await _personService.CreatePersonAsync(person);

                if (createdPerson != null)
                {
                    // La persona se creó exitosamente. Muestra un mensaje de éxito.
                    Console.WriteLine("CreatePerson: Persona creada con éxito.");
                    await Application.Current.MainPage.DisplayAlert("Éxito", "La persona se creó exitosamente.", "Aceptar");
                }
                else
                {
                    // Hubo un error al crear la persona. Muestra un mensaje de error.
                    Console.WriteLine("CreatePerson: Error al crear persona.");
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error al crear la persona. Verifica los datos e intenta nuevamente.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                // Establece el mensaje de error en la propiedad ErrorMessage
                ErrorMessage = "Error: " + ex.Message;

                // Muestra un mensaje de error en la consola
                Console.WriteLine("CreatePerson: Error inesperado: " + ex.Message);

                // Muestra un mensaje de error al usuario
                await Application.Current.MainPage.DisplayAlert("Error", "Se produjo un error inesperado al crear la persona: " + ex.Message, "Aceptar");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
