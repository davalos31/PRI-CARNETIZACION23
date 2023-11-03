namespace PetPass.View;
using PetPass.Model;
using PetPass.Validation;
using PetPass.Service;
using PetPass.View.Person;

public partial class CreatePerson : ContentPage
{
    int _userId;
    string _token;
    private readonly PersonService personService;
    string _base = Validations.GetCapturedImageBase64();
    public CreatePerson(int _id, string token)
    {
        InitializeComponent();
        _userId = _id;
        _token = token;
        personService = new PersonService();
    }




    private async void CreatePersonButton_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string firtname = FirtsEntry.Text;
        string lastName = LastNameEntry.Text;
        string ci = CIEntry.Text;
        string gender = GenderEntry.Text;
        string address = AddressEntry.Text;
        string phone = PhoneEntry.Text;
        string email = EmailEntry.Text;
        int state = 1;
        int userId = _userId;
        string base64Image = Validations.GetCapturedImageBase64();
        // Realiza las validaciones
        Validations val = new Validations();
        (bool isNameValid, string nameError) = val.ValidateName(name);
        (bool isFirtsNameValid, string FirtsnameError) = val.ValidateFirstName(firtname);
        (bool isLastNameValid, string LastnameError) = val.ValidateLastName(lastName);
        (bool isGenderValid, string genderError) = val.ValidateGender(gender);
        (bool isImageValid, string imageError) = val.ValidateImage(base64Image);
        (bool isPhoneValid, string phoneError) = val.ValidatePhone(phone);
        (bool isCIValid, string ciError) = val.ValidateCI(ci);
        (bool isEmailValid, string emailError) = val.ValidateEmail(email);

        if (isNameValid && isPhoneValid && isCIValid && isEmailValid && isFirtsNameValid && isLastNameValid && isGenderValid && isImageValid)
        {
            try
            {
                // Supongamos que 'Validations.GetCapturedImageBase64()' contiene la imagen en formato base64


                var person = new PersonRegister
                {
                    Name = name,
                    FirstName = firtname,
                    LastName = lastName,
                    CI = ci,
                    Gender = gender,
                    Address = address,
                    Phone = int.Parse(phone),
                    Email = email,
                    State = state,
                    UserID = userId,
                    Image = base64Image
                };

                // Obtén el token de autorización de tu sistema de autenticación
                string authToken = _token;

                // Llama al método CreateBrigadier de PersonService
                var createdPerson = await personService.CreatePersonAsync(person, authToken);

                if (createdPerson != null)
                {
                    // El registro de persona fue exitoso, realiza acciones necesarias
                    await DisplayAlert("Éxito", "Jefe Brigada creado con éxito.", "OK");
                    Clear();
                    Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
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
            if (!isImageValid) errorMessage += $"- {imageError}\n";

            await DisplayAlert("Error", errorMessage, "OK");
        }
    }


    private async void CamerButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ImagePerson());
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