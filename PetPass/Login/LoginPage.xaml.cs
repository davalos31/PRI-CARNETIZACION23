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
		// Navegar a la página de recuperación de contraseña
		Navigation.PushAsync(new RequireEmail());
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		// Validar que el campo de correo electrónico no esté vacío
		if (string.IsNullOrWhiteSpace(UserEntry.Text))
		{
			await DisplayAlert("Error", "Por favor, ingrese su correo electrónico.", "OK");
			return;
		}

		// Validar que el campo de contraseña no esté vacío
		if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
		{
			await DisplayAlert("Error", "Por favor, ingrese su contraseña.", "OK");
			return;
		}

		// antes de mandar debe recibir el token, id, rol, etc
		AuthResponse res = await LS.Login(UserEntry.Text, PasswordEntry.Text);
		if (res != null)
		{
			// Aquí puedes agregar la lógica de autenticación y manejo del inicio de sesión
			// Si las validaciones pasan, realiza la autenticación y navega a la siguiente página
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
				else if (res.Role == 'O')// dueño
				{
					await DisplayAlert("mensaje", "No hay ventana de dueño.", "OK");
				}
			}
			else
			{
				await Navigation.PushAsync(new FirstLogin());
			}

		}
		else
		{
			await DisplayAlert("Error", "Usaurio o contraseña no validos.", "OK");
		}
	}
}