using PetPass.Model;
using PetPass.Service;

namespace PetPass;

public partial class RegisterPet : ContentPage
{
	private readonly IMediaPicker mediaPicker;
	readonly PetService pets;
	private int id;
	
	public RegisterPet(int personId)
	{
		InitializeComponent();
		pets = new PetService();
		id = personId;

	}



	public async void Button_Clicked_1(object sender, EventArgs e)
	{
        try
        {
            Pet p = new(0, Name.Text, Specie.SelectedItem.ToString(), Breed.Text, Gender.SelectedItem.ToString()[0], BirthDate.Date, SpecialFeature.Text, 0, id);
            var page = new UseCamera(p);
            await Navigation.PushAsync(page);
        }
        catch
        {
            await DisplayAlert("Sistema", "no se pudo completar el registro1", "ok");
        }
        
	}
}