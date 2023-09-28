namespace PetPass.Inicio_de_sesion;
using Microsoft.Maui.Controls;

public partial class Session : ContentPage
{
    //este esl para el inicio sesion
	public Session()
	{
		InitializeComponent();
	}

    private void ForgotPassword_Tapped(object sender, EventArgs e)
    {
        // Navegar a la página de recuperación de contraseña
        Navigation.PushAsync(new NewPassword());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Validar que el campo de correo electrónico no esté vacío
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su correo electrónico.", "OK");
            return;
        }

        // Validar que el campo de contraseña no esté vacío
        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su contraseña.", "OK");
            return;
        }

        // Aquí puedes agregar la lógica de autenticación y manejo del inicio de sesión
        // Si las validaciones pasan, realiza la autenticación y navega a la siguiente página
        var Main = new MainPage();
        Navigation.PushAsync(Main);
    }
}
