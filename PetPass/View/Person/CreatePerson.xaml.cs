namespace PetPass.View;
using PetPass.Model;
using PetPass.Service;


public partial class CreatePerson : ContentPage
{
    int _userId;
    string _token;
    private readonly PersonService personService;
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




        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email))
        {
            try
            {
                var person = new PersonRegister
				{
                    Name = name,
                    FirstName = firtname,
                    LastName = lastName,
                    CI= ci,
                    Gender=gender,
                    Address=address,
                    Phone = int.Parse(phone),
                    Email = email,
                    State=1,
                    UserID =userId
                };

                // Obtén el token de autorización de tu sistema de autenticación
                string authToken = _token; 

                // Llama al método CreatePersonAsync de PersonService
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
            await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
        }
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