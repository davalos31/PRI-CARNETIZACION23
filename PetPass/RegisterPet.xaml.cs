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

	private async void Button_Clicked(object sender, EventArgs e)
	{
		try
		{
			Pet p = new(0, Name.Text, Specie.SelectedItem.ToString(), Breed.Text, Gender.SelectedItem.ToString()[0], BirthDate.Date, SpecialFeature.Text, 0, id);



			bool res = await pets.CreatePet(p);
			if (res)
			{
				await DisplayAlert("Sistema", "el registro se completo correctamente", "ok");

				await Navigation.PushAsync(new MenuBrigadier());
			}
			else
			{
				await DisplayAlert("Sistema", "no se pudo completar el registro2", "ok");
			}



		}
		catch
		{
			await DisplayAlert("Sistema", "no se pudo completar el registro1", "ok");
		}
	}

	private void Button_Clicked_1(object sender, EventArgs e)
	{
		var page = new UseCamera();
		Navigation.PushAsync(page);
	}
}