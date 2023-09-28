namespace PetPass.Inicio_de_sesion;

public partial class NewPassword : ContentPage
{
    //este es el cambio de contraseña
	public NewPassword()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Validar que el campo de nueva contraseña no esté vacío
        if (string.IsNullOrWhiteSpace(NewPasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su nueva contraseña.", "OK");
            return;
        }

        // Validar que el campo de confirmación de contraseña no esté vacío
        if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, confirme su nueva contraseña.", "OK");
            return;
        }

        // Validar que la nueva contraseña y la confirmación sean iguales
        if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            DisplayAlert("Error", "Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "OK");
            return;
        }

        // Aquí puedes agregar la lógica para cambiar la contraseña
        // Si las validaciones pasan, realiza el cambio de contraseña
        var main2 = new MainPage();
        Navigation.PushAsync(main2);
    }
}