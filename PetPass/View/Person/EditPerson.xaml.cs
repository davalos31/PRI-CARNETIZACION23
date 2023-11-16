


using PetPass.Model;
using PetPass.Service;
using PetPass.Validation;

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
            LastNameEntry.Text = person.LastName;
            CIEntry.Text = person.CI;
            GenderPicker.SelectedItem = person.Gender;
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
            // Deshabilitar los botones antes de iniciar el proceso
            AssignPhotoButton.IsEnabled = false;
            UpdateCampaignButton.IsEnabled = false;

            string base64Image = Validations.GetCapturedImageBase64();

            var person = new PersonRegister
            {
                PersonId = _idPerson,
                Name = NameEntry.Text,
                FirstName = FirtsEntry.Text,
                LastName = LastNameEntry.Text,
                CI = CIEntry.Text,
                Gender = GenderPicker.SelectedItem as string,
                Address = AddressEntry.Text,
                Phone = int.Parse(PhoneEntry.Text),
                Email = EmailEntry.Text,
                State = 1,
                UserID = _idUserValue,
                Image = base64Image
            };

            string authToken = _token;

            Validations val = new Validations();
            (bool isNameValid, string nameError) = val.ValidateName(person.Name);
            (bool isFirtsNameValid, string FirtsnameError) = val.ValidateFirstName(person.FirstName);
            (bool isLastNameValid, string LastnameError) = val.ValidateLastName(person.LastName);
            (bool isGenderValid, string genderError) = val.ValidateGender(person.Gender);
            (bool isPhoneValid, string phoneError) = val.ValidatePhone(person.Phone.ToString());
            (bool isCIValid, string ciError) = val.ValidateCI(person.CI);
            (bool isEmailValid, string emailError) = val.ValidateEmail(person.Email);

            if (isNameValid && isPhoneValid && isCIValid && isEmailValid && isFirtsNameValid && isLastNameValid && isGenderValid /*&& isImageValid*/)
            {
                // Llama al método UpdatePersonAsync de PersonService
                bool updatePerson = await _personService.UpdatePersonAsync(_token, person);

                if (updatePerson)
                {
                    DisplayAlert("Éxito", "Jefe Brigada Actualizado correctamente.", "OK");

                    Navigation.PushAsync(new MenuMain(_idUserValue, _token));
                }
                else
                {
                    DisplayAlert("Error", "No se pudo actualizar el Jefe Brigada.", "OK");
                }
            }
            else
            {
                // Muestra un mensaje de error con los detalles de las validaciones fallidas
                string errorMessage = "Por favor, corrija los siguientes errores:\n";
                if (!isNameValid) errorMessage += $"- {nameError}\n";
                if (!isPhoneValid) errorMessage += $"- {phoneError}\n";
                if (!isCIValid) errorMessage += $"- {ciError}\n";
                if (!isEmailValid) errorMessage += $"- {emailError}\n";
                if (!isFirtsNameValid) errorMessage += $"- {FirtsnameError}\n";
                if (!isLastNameValid) errorMessage += $"- {LastnameError}\n";
                if (!isGenderValid) errorMessage += $"- {genderError}\n";
                //if (!isImageValid) errorMessage += $"- {imageError}\n";

                await DisplayAlert("Error", errorMessage, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            // Habilitar los botones después de completar el proceso, incluso si hay una excepción
            AssignPhotoButton.IsEnabled = true;
            UpdateCampaignButton.IsEnabled = true;
        }
    }
    private async void CamerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Deshabilitar los botones antes de iniciar el proceso
            AssignPhotoButton.IsEnabled = false;
            UpdateCampaignButton.IsEnabled = false;

            await Navigation.PushAsync(new ImagePerson());
        }
        finally
        {
            // Habilitar los botones después de mostrar la página, incluso si hay una excepción
            AssignPhotoButton.IsEnabled = true;
            UpdateCampaignButton.IsEnabled = true;
        }
    }
}