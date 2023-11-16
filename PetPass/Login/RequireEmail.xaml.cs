using PetPass.Service;

namespace PetPass.Login;

public partial class RequireEmail : ContentPage
{
	private readonly LoginService ls;
	public RequireEmail()
	{
		InitializeComponent();
		ls = new LoginService();
	}

	private async void btnRecovery_Clicked(object sender, EventArgs e)
	{
		loadingIndicator.IsRunning = true;
		loadingIndicator.IsVisible = true;
		btnRecovery.IsEnabled = false;

		try
		{
			int UserId = await ls.FindByEmail(EmailEntry.Text);
			if (UserId > 0)
			{
				var form = new RestorePassword(UserId);
				await Navigation.PushAsync(form);
			}
			else
			{
				await DisplayAlert("Sistema", "no se encontro el email ingresado", "ok");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
			// Ocultar la pantalla de carga, incluso si hay una excepción
			loadingIndicator.IsRunning = false;
			loadingIndicator.IsVisible = false;
			btnRecovery.IsEnabled = true;
		}
	}
}