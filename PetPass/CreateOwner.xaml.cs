using Microsoft.Win32;
using PetPass.Model;
using PetPass.Service;
using PetPass.Validation;
namespace PetPass;

public partial class CreateOwner : ContentPage
{
	string[] Sexos = { "Masculino", "Femenino", "Otro" };
	readonly ServicePeople SP;
	int userId = 10;

	public CreateOwner()
	{
		InitializeComponent();
		loadPicker();
		SP = new ServicePeople();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
	}


	void loadPicker()
	{
		EntryGender.ItemsSource = Sexos;
	}

	private void EntryPhone_TextChanged(object sender, TextChangedEventArgs e)
	{
		// solo numeros
		if (EntryPhone.Text.Length > 0)
			if (EntryPhone.Text.Any(x => !Char.IsNumber(x)))
			{
				EntryPhone.Text = EntryPhone.Text.Substring(0, EntryPhone.Text.Length - 1);
			}

	}

	private async void btnVolver_Clicked(object sender, EventArgs e)
	{
		bool resultado = await DisplayAlert("Sistema", "¿Seguro que quiere salir del formulario?", "Si", "No");

		if (resultado)
		{
			await Navigation.PopAsync();
		}
	}

	private async void btnRegistrar_Clicked(object sender, EventArgs e)
	{
		bool valid = Validation();

		if (valid)
		{
			try
			{
				Person p = new(0, EntryName.Text, EntryFirstName.Text, EntryLastName.Text, EntryCI.Text, Sexos[EntryGender.SelectedIndex][0].ToString(), EntryAddress.Text, int.Parse(EntryPhone.Text), EntryEmail.Text, 1);

				bool res = await SP.CreateOwner(p, userId);
				if (res)
				{
					await DisplayAlert("Sistema", "el registro se completo correctamente", "ok");
					var page = new MainPage();
					await Navigation.PushAsync(page);
				}
				else
				{
					await DisplayAlert("Sistema", "no se pudo completar el registro", "ok");
				}

			}
			catch
			{
				await DisplayAlert("Sistema", "no se pudo completar el registro", "ok");
			}

		}
	}

	bool Validation()
	{
		(bool res, string msg) validacion;
		Validations val = new Validations();

		validacion = val.ValidateName(EntryName.Text);
		if (!validacion.res)
		{
			DisplayAlert("Error", "el nombre " + validacion.msg, "ok");
			return false;
		}

		validacion = val.ValidateName(EntryFirstName.Text);
		if (!validacion.res)
		{
			DisplayAlert("Error", "el primer apellido " + validacion.msg, "ok");
			return false;
		}

		if (!string.IsNullOrWhiteSpace(EntryLastName.Text))
		{
			validacion = val.ValidateName(EntryLastName.Text);
			if (!validacion.res)
			{
				DisplayAlert("Error", "el segundo apellido " + validacion.msg, "ok");
				return false;
			}
		}

		validacion = val.ValidateCI(EntryCI.Text);
		if (!validacion.res)
		{
			DisplayAlert("Error", validacion.msg, "ok");
			return false;
		}

		validacion = val.ValidateAgeOver18(EntryBirthDate.Date);
		if (!validacion.res)
		{
			DisplayAlert("Error", "la fecha de nacimiento " + validacion.msg, "ok");
			return false;
		}

		if (EntryGender.SelectedIndex == -1)
		{
			DisplayAlert("Error", "el sexo es obligatorio", "ok");
			return false;
		}


		if (string.IsNullOrEmpty(EntryAddress.Text))
		{
			DisplayAlert("Error", "la direccion es obligatoria", "ok");
			return false;
		}


		validacion = val.ValidatePhone(EntryPhone.Text);
		if (!validacion.res)
		{
			DisplayAlert("Error", validacion.msg, "ok");
			return false;
		}

		validacion = val.ValidateEmail(EntryEmail.Text);
		if (!validacion.res)
		{
			DisplayAlert("Error", validacion.msg, "ok");
			return false;
		}

		return true;
	}
}