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



namespace PetPass.ViewModel
{
    public class PersonViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private readonly IPersonService _personService;

        public PersonViewModel()
        {
            _personService = new PersonService();
            CreatePersonCommand = new AsyncCommand(CreatePersonAsync);
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

        public ICommand CreatePersonCommand { get; private set; }
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

        private async Task CreatePersonAsync()
        {




    

            //// Validar los datos del objeto Person utilizando ValidationForm
            //List<string> validationErrors = ValidationForm.ValidatePerson(person);

            //if (validationErrors.Count > 0)
            //{
            //    // Hay errores de validación, muestra los mensajes en una alerta
            //    string errorMessage = string.Join("\n", validationErrors);
            //    await Application.Current.MainPage.DisplayAlert("Error de validación", errorMessage, "OK");
            //    return;
            //}

            var person = new Person
            {
                Name = Name,
                FirstName = FirstName,
                LastName = LastName,
                CI = CI,
                Gender = Gender,
                Address = Address,
                Phone = Phone,
                Email = Email
            };

            // No hay errores de validación, intenta crear la persona
            Person createdPerson = await _personService.CreatePersonAsync(person);

            if (createdPerson != null)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Persona creada correctamente.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la persona. Inténtalo de nuevo.", "OK");
            }
        }
    }
}
