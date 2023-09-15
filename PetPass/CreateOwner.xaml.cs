using PetPass.Validation;
namespace PetPass;

public partial class CreateOwner : ContentPage
{
    string[] Sexos = { "Masculino", "Femenino", "Otro" };
    public CreateOwner()
    {
        InitializeComponent();
        loadPicker();
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
            var main = new MainPage();
            await Navigation.PushAsync(main);
        }
    }

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        bool valid = true;
        (bool res, string msg) validacion;
        Validations val = new Validations();


        #region validation
        validacion = val.ValidateName(EntryName.Text);
        if (!validacion.res)
        {
            DisplayAlert("Error", "el nombre " + validacion.msg, "ok");
            valid = false;
        }

        if (valid)
        {
            validacion = val.ValidateName(EntryFirstName.Text);
            if (!validacion.res)
            {
                DisplayAlert("Error", "el primer apellido " + validacion.msg, "ok");
                valid = false;
            }
        }

        if (valid && !string.IsNullOrEmpty(EntryLastName.Text))
        {
            validacion = val.ValidateName(EntryLastName.Text);
            if (!validacion.res)
            {
                DisplayAlert("Error", "el segundo apellido " + validacion.msg, "ok");
                valid = false;
            }
        }

        if (valid)
        {
            validacion = val.ValidateCI(EntryCI.Text);
            if (!validacion.res)
            {
                DisplayAlert("Error", validacion.msg, "ok");
                valid = false;
            }
        }

        if (valid)
        {
            validacion = val.ValidateDate(EntryBirthDate.Date);
            if (!validacion.res)
            {
                DisplayAlert("Error", "la fecha de nacimiento " + validacion.msg, "ok");
                valid = false;
            }
        }

        if (valid)
            if (EntryGender.SelectedIndex == -1)
            {
                DisplayAlert("Error", "el sexo es obligatorio", "ok");
                valid = false;
            }

        if (valid)
            if (string.IsNullOrEmpty(EntryAddress.Text))
            {
                DisplayAlert("Error", "la direccion es obligatoria", "ok");
                valid = false;
            }

        if (valid)
        {
            validacion = val.ValidatePhone(EntryPhone.Text);
            if (!validacion.res)
            {
                DisplayAlert("Error", validacion.msg, "ok");
                valid = false;
            }
        }

        if (valid)
        {
            validacion = val.ValidateEmail(EntryEmail.Text);
            if (!validacion.res)
            {
                DisplayAlert("Error", validacion.msg, "ok");
                valid = false;
            }
        }
        #endregion

        if (valid)
        {
            DisplayAlert("Sistema", "el registro se completo correctamente", "ok");
            //guardar
        }
    }
}