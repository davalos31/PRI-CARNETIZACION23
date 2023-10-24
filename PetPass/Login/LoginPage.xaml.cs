namespace PetPass.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void ForgotPassword_Tapped(object sender, EventArgs e)
	{
		// Navegar a la página de recuperación de contraseña
		Navigation.PushAsync(new RequireEmail());
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

		// antes de mandar debe recibir el token, id, rol, etc
		//
		//


		// Aquí puedes agregar la lógica de autenticación y manejo del inicio de sesión
		// Si las validaciones pasan, realiza la autenticación y navega a la siguiente página
		var Main = new MenuBrigadier();
		Navigation.PushAsync(Main);

		// falta mandar segun el rol
	}
}