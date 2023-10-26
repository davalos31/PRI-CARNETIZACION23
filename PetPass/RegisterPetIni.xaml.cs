using PetPass.Model;
using PetPass.Service;

namespace PetPass;

public partial class RegisterPetIni : ContentPage
{
    private ServicePeople servicePeople;
	Person person;
	public RegisterPetIni()
	{
		InitializeComponent();
        servicePeople = new ServicePeople();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        person = await servicePeople.findbyCI(EntryCi.Text);

        if ( person == null)
		{
			var OwnerNot = new OwnerNotFound();
			await Navigation.PushAsync(OwnerNot);
			return;
		}

		Name.Text = person.Name;
		FirstName.Text = person.FirstName;
		LastName.Text = person.LastName;

	}

	private void btnCreate_Clicked(object sender, EventArgs e)
	{
		if (person != null)
		{
			var OwnerNot = new RegisterPet(person.PersonId);
			Navigation.PushAsync(OwnerNot);
		}
	}
}