namespace PetPass;

public partial class RegisterPet : ContentPage
{
    private readonly IMediaPicker mediaPicker;

    public RegisterPet()
	{
		InitializeComponent();
        
    }
    

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Validar que el nombre no est� vac�o
        if (string.IsNullOrWhiteSpace(NombreEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese un nombre v�lido.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado una especie
        if (EspeciePicker.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Por favor, seleccione una especie.", "Aceptar");
            return;
        }

        // Validar que la raza no est� vac�a
        if (string.IsNullOrWhiteSpace(RazaEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese una raza v�lida.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado un g�nero
        if (!(GeneroMachoRadioButton.IsChecked || GeneroHembraRadioButton.IsChecked))
        {
            DisplayAlert("Error", "Por favor, seleccione un g�nero.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado una fecha de nacimiento
        if (FechaNacimientoDatePicker.Date == DateTime.MinValue)
        {
            DisplayAlert("Error", "Por favor, seleccione una fecha de nacimiento v�lida.", "Aceptar");
            return;
        }

        // Validar que el estado est� seleccionado
        if (EstadoPicker.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Por favor, seleccione un estado.", "Aceptar");
            return;
        }

        // Si pasa todas las validaciones, procesa los datos
        string nombre = NombreEntry.Text;
        string especie = EspeciePicker.SelectedItem.ToString();
        string raza = RazaEntry.Text;
        string genero = GeneroMachoRadioButton.IsChecked ? "Macho" : "Hembra";
        DateTime fechaNacimiento = FechaNacimientoDatePicker.Date;
        string rasgoEspecial = RasgoEspecialEntry.Text;
        string estado = EstadoPicker.SelectedItem.ToString();

        // Aqu� puedes hacer lo que necesites con los datos ingresados

        DisplayAlert("�xito", "Registro de mascota exitoso.", "Aceptar");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var page = new UseCamera();
        Navigation.PushAsync(page);
    }
}