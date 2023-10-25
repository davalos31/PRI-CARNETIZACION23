namespace PetPass;

public partial class RegisterPetIni : ContentPage
{
	public RegisterPetIni()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var OwnerNot = new OwnerNotFound();
        Navigation.PushAsync(OwnerNot);
    }
}