using PetPass.Service;

namespace PetPass;

public partial class AskRecovery : ContentPage
{
	private readonly RestoreService rs;
	public AskRecovery()
	{
		InitializeComponent();
		rs = new RestoreService();
	}

	private async void btnRecovery_Clicked(object sender, EventArgs e)
	{
		try
		{
			int UserId = await rs.FindByEmail(EmailEntry.Text);
			if (UserId > 0)
			{
				var form = new RestorePassword(UserId);
				await Navigation.PushAsync(form);
			}
			else
			{
				await DisplayAlert("Sistema", "no se encontro el email ingresado", "ok");
			}
		} catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}