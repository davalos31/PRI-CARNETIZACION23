using PetPass.Model.Extras;
using PetPass.Service;
using PetPass.View;

namespace PetPass.Login;

public partial class LoginPage : ContentPage
{
	private LoginService LS;
	public LoginPage()
	{
		InitializeComponent();
		LS = new LoginService();
	}

	private void ForgotPassword_Tapped(object sender, EventArgs e)
	{
		// Navegar a la p�gina de recuperaci�n de contrase�a
		Navigation.PushAsync(new RequireEmail());
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		// Validar que el campo de correo electr�nico no est� vac�o
		if (string.IsNullOrWhiteSpace(UserEntry.Text))
		{
			await DisplayAlert("Error", "Por favor, ingrese su correo electr�nico.", "OK");
			return;
		}

		// Validar que el campo de contrase�a no est� vac�o
		if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
		{
			await DisplayAlert("Error", "Por favor, ingrese su contrase�a.", "OK");
			return;
		}

		// antes de mandar debe recibir el token, id, rol, etc
		AuthResponse res = await LS.Login(UserEntry.Text, PasswordEntry.Text);
		if (res != null)
		{
			// Aqu� puedes agregar la l�gica de autenticaci�n y manejo del inicio de sesi�n
			// Si las validaciones pasan, realiza la autenticaci�n y navega a la siguiente p�gina
			session.AuthResponse = res;
			if (!res.FirstLogin)
			{
				if (res.Role == 'A') //admin
				{
					await Navigation.PushAsync(new MenuMain(session.AuthResponse.userID, session.AuthResponse.Token,session.AuthResponse.Photo));
				}
				else if (res.Role == 'B')//brigadier
				{
					await Navigation.PushAsync(new MenuBrigadier());
				}
				else if (res.Role == 'O')// due�o
				{
					await DisplayAlert("mensaje", "No hay ventana de due�o.", "OK");
				}
			}
			else
			{
				await Navigation.PushAsync(new FirstLogin());
			}

		}
		else
		{
			await DisplayAlert("Error", "Usaurio o contrase�a no validos.", "OK");
		}
	}
}