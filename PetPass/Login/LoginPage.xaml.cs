namespace PetPass.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void ForgotPassword_Tapped(object sender, EventArgs e)
	{
		// Navegar a la p�gina de recuperaci�n de contrase�a
		Navigation.PushAsync(new RequireEmail());
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

		// antes de mandar debe recibir el token, id, rol, etc
		//
		//


		// Aqu� puedes agregar la l�gica de autenticaci�n y manejo del inicio de sesi�n
		// Si las validaciones pasan, realiza la autenticaci�n y navega a la siguiente p�gina
		var Main = new MenuBrigadier();
		Navigation.PushAsync(Main);

		// falta mandar segun el rol
	}
}