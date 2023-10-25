namespace PetPass;

public partial class OwnerNotFound : ContentPage
{
	public OwnerNotFound()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var formPet = new RegisterPetIni();
        Navigation.PushAsync(formPet);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var formOwner = new CreateOwner();
        Navigation.PushAsync(formOwner);
    }
}