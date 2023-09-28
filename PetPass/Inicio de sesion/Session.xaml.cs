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
        // Navegar a la p�gina de recuperaci�n de contrase�a
        Navigation.PushAsync(new NewPassword());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Validar que el campo de correo electr�nico no est� vac�o
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su correo electr�nico.", "OK");
            return;
        }

        // Validar que el campo de contrase�a no est� vac�o
        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese su contrase�a.", "OK");
            return;
        }

        // Aqu� puedes agregar la l�gica de autenticaci�n y manejo del inicio de sesi�n
        // Si las validaciones pasan, realiza la autenticaci�n y navega a la siguiente p�gina
        var Main = new MainPage();
        Navigation.PushAsync(Main);
    }
}
