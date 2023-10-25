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

        public PersonViewModel(IPersonService personService, string _tokenValue)
        {
            _personService = personService;
            Token = _tokenValue;
            PagesLogin = new AsyncCommand(async () => await Login());
            LoadPersonAsync();


        }

        private string _token;
        public string Token
        {
            get => _token;
            set
            {
                if (_token != value)
                {
                    _token = value;
                    OnPropertyChanged();
                }
            }
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


        private int _personId;
        public int PersonID
        {
            get => _personId;
            set
            {
                if (_personId != value)
                {
                    _personId = value;
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


      


        private List<Persons> _people;
        public List<Persons> People
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

     

        public ICommand PagesLogin { get; set; }
       



        public async Task LoadPersonAsync()
        {
            try
            {
                List<Persons> PeopleList = await _personService.GetPeopleAsync(Token);
                People = PeopleList;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error las Personas: " + ex.Message);
            }
        }





        private async Task Login()
        {
            //await App.Current.MainPage.Navigation.PushAsync(new Login());
           // await App.Current.MainPage.Navigation.PushAsync(new DetailPatrol());
        }


 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
