using PetPass.Model;
using PetPass.Service;
using System.Runtime.ExceptionServices;

namespace PetPass.View.Person;

public partial class EditPerson : ContentPage
{
    private PersonService _personService;
    int _idPerson;
    string _token;
    int _idUserValue;
    public EditPerson(int _idperson, string token, int _idUser)
	{
		InitializeComponent();
        _idPerson = _idperson;
        _token = token;
        _idUserValue = _idUser;
        _personService = new PersonService();
        LoadPersonDetails(_idPerson);
	}

    private async void LoadPersonDetails(int Id)
    {
        var person = await _personService.GetPersonDetailsAsync(_token, Id);

        if (person != null)
        {
         NameEntry.Text = person.Name;
         FirtsEntry.Text = person.FirstName;
         LastNameEntry.Text =person.LastName;
          CIEntry.Text = person.CI;
           GenderEntry.Text = person.Gender;
           AddressEntry.Text = person.Address;
            PhoneEntry.Text = person.Phone.ToString();
            EmailEntry.Text = person.Email;
         
          
        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar los detalles de la Persona.", "OK");
            
        }
    }

    private async void UpdateCampaignButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var person = new Persons
            {
                PersonId = _idPerson,
                Name = NameEntry.Text,
                FirstName = FirtsEntry.Text,
                LastName = LastNameEntry.Text,
                CI = CIEntry.Text,
                Gender = GenderEntry.Text,
                Address = AddressEntry.Text,
                Phone = int.Parse(PhoneEntry.Text),
                Email = EmailEntry.Text,
                State = 1,
                UserID = _idUserValue
            };

            
            string authToken = _token;

            // Llama al método CreatePersonAsync de PersonService
            bool updatePerson = await _personService.UpdatePersonAsync(_token, person);

            if (updatePerson)
            {
                DisplayAlert("Éxito", "Jefe Brigada Actualizado correctamente.", "OK");
                Clear();
                Navigation.PushAsync(new MenuMain(_idUserValue, _token));
            }
            else
            {
                DisplayAlert("Error", "No se pudo actualizar el Jefe Brigada.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

        void Clear()
        {
            NameEntry.Text = "";
            FirtsEntry.Text = "";
            LastNameEntry.Text = "";
            CIEntry.Text = "";
            GenderEntry.Text = "";
            AddressEntry.Text = "";
            PhoneEntry.Text = "";
            EmailEntry.Text = "";
        }

    }
}