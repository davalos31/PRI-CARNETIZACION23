namespace PetPass.View;

using PetPass.Service;
using PetPass.ViewModel;
public partial class CreatePerson : ContentPage
{
    public CreatePerson()
    {
        InitializeComponent();
        // Obtén una instancia de IPersonService utilizando DependencyService
        var personService = new PersonService(); // Reemplaza "PersonService" con el nombre de tu clase real.

        BindingContext = new PersonViewModel(personService);
    }
}