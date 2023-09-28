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

        public ICommand PagesLogin{ get; set; }
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
            await App.Current.MainPage.Navigation.PushAsync(new Login());
        }


        private async Task CreatePerson()
        {
            // Crea un objeto Person con los datos del formulario
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
            };

            // Llama al método para crear la persona en el servicio
            Person createdPerson = await _personService.CreatePersonAsync(person);

            // Verifica si la persona se creó exitosamente o si hubo un error
            if (createdPerson != null)
            {
                // La persona se creó exitosamente. Muestra un mensaje al usuario.
                await Application.Current.MainPage.DisplayAlert("Éxito", "La persona se creó exitosamente.", "Aceptar");
            }
            else
            {
                // Hubo un error al crear la persona. Muestra un mensaje de error al usuario.
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error al crear la persona.", "Aceptar");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
