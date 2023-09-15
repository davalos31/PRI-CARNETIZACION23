namespace PetPass.View;
using PetPass.ViewModel;
public partial class CreatePerson : ContentPage
{
    public CreatePerson()
    {
        InitializeComponent();
        BindingContext = new PersonViewModel();
    }
}