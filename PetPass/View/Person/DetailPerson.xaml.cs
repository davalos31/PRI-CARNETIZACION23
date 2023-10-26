using PetPass.Service;
using PetPass.Model;


namespace PetPass.View.Person;

public partial class DetailPerson : ContentPage
{

    private PersonService _personService;
    int _idPerson;
    int _userId;
    string _token;
    
    public DetailPerson(string token, int _idUser)
	{
		InitializeComponent();
        _token = token;
        _userId = _idUser;
        _personService = new PersonService();
        LoadPersonData();
    }






    private async void LoadPersonData()
    {
        try
        {
            var personService = new PersonService();
            List<PersonRegister> personDataList = await personService.GetPeopleAsync(_token);

            if (personDataList != null && personDataList.Count > 0)
            {
                // Filtra los datos para mostrar solo aquellos con estado igual a 1
                var filteredData = personDataList.Where(person => person.State == 1).ToList();

                if (filteredData.Count > 0)
                {
                    PersonDataListView.ItemsSource = filteredData;
                }
                else
                {
                    await DisplayAlert("Advertencia", "No se encontraron datos de las personas ", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "No se encontraron datos de las personas", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Ocurrió un error al cargar los datos de las personas.", "OK");
        }
    }
    private void Edit(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int PersonId)
        {
            string userIDValue = PersonId.ToString(); 
            DisplayAlert("ID Guardado", "El valor de la persona es: " + userIDValue, "OK");
            Navigation.PushAsync(new EditPerson(PersonId,_token,_userId));
        }
        else
        {
            DisplayAlert("Error", "No se ha seleccionado un elemento válido.", "OK");
        }

    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is int personID)
        {
            bool result = await DisplayAlert("Confirmación", "¿Está seguro de eliminar el registro?", "Sí", "No");

            if (result)
            {
                string authToken = _token;

                // Llama al método DeletePersonAsync de PersonService
                bool deletePerson = await _personService.DeletePersonAsync(authToken, personID);

                if (deletePerson)
                {
                    await DisplayAlert("Éxito", "Jefe Brigada Eliminado correctamente.", "OK");
                    LoadPersonData();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el Jefe Brigada.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "No se ha seleccionado un elemento válido.", "OK");
        }

    }

    private async void Volver_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}