using PetPass.Model.Extras;
using PetPass.Service;

namespace PetPass.Login;

public partial class FirstLogin : ContentPage
{
	private LoginService LS;
	public FirstLogin()
	{
		InitializeComponent();
		LS = new LoginService();
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		loadingIndicator.IsRunning = true;
		loadingIndicator.IsVisible = true;
		btnCambiar.IsEnabled = false;

		try
		{
			// Validar que el campo de nueva contrase�a no est� vac�o
			if (string.IsNullOrWhiteSpace(NewPasswordEntry.Text))
			{
				await DisplayAlert("Error", "Por favor, ingrese su nueva contrase�a.", "OK");
				return;
			}

			// Validar que el campo de confirmaci�n de contrase�a no est� vac�o
			if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
			{
				await DisplayAlert("Error", "Por favor, confirme su nueva contrase�a.", "OK");
				return;
			}

			// Validar que la nueva contrase�a y la confirmaci�n sean iguales
			if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
			{
				await DisplayAlert("Error", "Las contrase�as no coinciden. Por favor, int�ntelo de nuevo.", "OK");
				return;
			}

			// Aqu� puedes agregar la l�gica para cambiar la contrase�a
			// Si las validaciones pasan, realiza el cambio de contrase�a
			if (await LS.FirstLogin(session.AuthResponse.userID, NewPasswordEntry.Text))
			{
				await DisplayAlert("Sistema", "Se cambio de contrase�a correctamente.", "OK");
				await Navigation.PushAsync(new LoginPage());
			}
			else
			{
				await DisplayAlert("Error", "No se pudo conectar con el servidor.", "OK");
			}
		}
		finally
		{
			loadingIndicator.IsRunning = false;
			loadingIndicator.IsVisible = false;
			btnCambiar.IsEnabled = true;
		}

	}
}