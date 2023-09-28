namespace PetPass.Inicio_de_sesion;

public partial class NewPassword : ContentPage
{
    //este es el cambio de contrase�a
	public NewPassword()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Validar que el campo de nueva contrase�a no est� vac�o
        if (string.IsNullOrWhiteSpace(NewPasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su nueva contrase�a.", "OK");
            return;
        }

        // Validar que el campo de confirmaci�n de contrase�a no est� vac�o
        if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, confirme su nueva contrase�a.", "OK");
            return;
        }

        // Validar que la nueva contrase�a y la confirmaci�n sean iguales
        if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            DisplayAlert("Error", "Las contrase�as no coinciden. Por favor, int�ntelo de nuevo.", "OK");
            return;
        }

        // Aqu� puedes agregar la l�gica para cambiar la contrase�a
        // Si las validaciones pasan, realiza el cambio de contrase�a
        var main2 = new MainPage();
        Navigation.PushAsync(main2);
    }
}