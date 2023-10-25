using PetPass.Service;

namespace PetPass.Login;

public partial class RestorePassword : ContentPage
{
	readonly LoginService ls;
	int UserId;

	public RestorePassword(int x)
	{
		InitializeComponent();
		UserId = x;
		ls = new LoginService();

	}

	private async void btnRestorePassword_Clicked(object sender, EventArgs e)
	{
		try
		{
			string res = Validate();
			if (res == "bien")
			{
				bool aux = await ls.RecoveryPassword(UserId, CodeEntry.Text, NewPasswordEntry.Text);

				if (aux)
				{
					await DisplayAlert("Sistema", "se cambio la contrase�a correctamente", "ok");
					var page = new LoginPage();
					await Navigation.PushAsync(page);
				}
				else
				{
					await DisplayAlert("Sistema", "revise el codigo", "ok");
				}
			}
			else
			{
				await DisplayAlert("dato no aceptado", res, "ok");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private string Validate()
	{
		if (string.IsNullOrEmpty(NewPasswordEntry.Text))
		{
			return "debe ingresar una nueva contrase�a.";
		}
		else if (NewPasswordEntry.Text.Count() < 8)
		{
			return "la contrase�a debe tener minimo 8 digitos.";
		}
		else if (NewPasswordEntry.Text != ConfirmPasswordEntry.Text)
		{
			return "las contrase�as no coinciden.";
		}
		else if (!NewPasswordEntry.Text.Any(x => char.IsLetter(x)))
		{
			return "la contrase�a debe tener una letra.";
		}
		else if (!NewPasswordEntry.Text.Any(x => char.IsNumber(x)))
		{
			return "la contrase�a debe tener un numero.";
		}
		else if (!NewPasswordEntry.Text.Any(x => !char.IsLetterOrDigit(x)))
		{
			return "la contrase�a debe tener un caracter especial.";
		}

		if (string.IsNullOrEmpty(CodeEntry.Text))
		{
			return "debe ingresar el codigo de recuperacion";
		}

		return "bien";
	}
}