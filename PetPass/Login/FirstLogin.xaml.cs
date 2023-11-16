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
			// Validar que el campo de nueva contraseña no esté vacío
			if (string.IsNullOrWhiteSpace(NewPasswordEntry.Text))
			{
				await DisplayAlert("Error", "Por favor, ingrese su nueva contraseña.", "OK");
				return;
			}

			// Validar que el campo de confirmación de contraseña no esté vacío
			if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
			{
				await DisplayAlert("Error", "Por favor, confirme su nueva contraseña.", "OK");
				return;
			}

			// Validar que la nueva contraseña y la confirmación sean iguales
			if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
			{
				await DisplayAlert("Error", "Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "OK");
				return;
			}

			// Aquí puedes agregar la lógica para cambiar la contraseña
			// Si las validaciones pasan, realiza el cambio de contraseña
			if (await LS.FirstLogin(session.AuthResponse.userID, NewPasswordEntry.Text))
			{
				await DisplayAlert("Sistema", "Se cambio de contraseña correctamente.", "OK");
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