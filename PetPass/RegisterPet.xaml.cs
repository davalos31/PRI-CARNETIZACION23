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
        // Validar que el nombre no esté vacío
        if (string.IsNullOrWhiteSpace(NombreEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese un nombre válido.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado una especie
        if (EspeciePicker.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Por favor, seleccione una especie.", "Aceptar");
            return;
        }

        // Validar que la raza no esté vacía
        if (string.IsNullOrWhiteSpace(RazaEntry.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese una raza válida.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado un género
        if (!(GeneroMachoRadioButton.IsChecked || GeneroHembraRadioButton.IsChecked))
        {
            DisplayAlert("Error", "Por favor, seleccione un género.", "Aceptar");
            return;
        }

        // Validar que se haya seleccionado una fecha de nacimiento
        if (FechaNacimientoDatePicker.Date == DateTime.MinValue)
        {
            DisplayAlert("Error", "Por favor, seleccione una fecha de nacimiento válida.", "Aceptar");
            return;
        }

        // Validar que el estado esté seleccionado
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

        // Aquí puedes hacer lo que necesites con los datos ingresados

        DisplayAlert("Éxito", "Registro de mascota exitoso.", "Aceptar");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var page = new UseCamera();
        Navigation.PushAsync(page);
    }
}