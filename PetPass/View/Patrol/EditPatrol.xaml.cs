using PetPass.Model;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace PetPass.View.Patrol;

public partial class EditPatrol : ContentPage
{
    private Patrol1 selectedPatrol;
    public EditPatrol(Patrol1 patrol)
	{

        InitializeComponent();
    //    selectedPatrol = patrol;

    //    // Obt�n una referencia a la instancia de CreatePatrol
    //    CreatePatrol createPatrolPage = (CreatePatrol)Application.Current.MainPage;

    //    // Accede a la lista de campa�as cargadas en la instancia de CreatePatrol
    //    ObservableCollection<Campaign> campaigns = createPatrolPage.Campaigns;

    //    // Configura el origen de datos del Picker con la lista de campa�as
    //    campaignPicker.ItemsSource = campaigns;

    //    // Accede a la lista de zonas cargadas en la instancia de CreatePatrol
    //    ObservableCollection<Zone> zones = createPatrolPage.Zones;

    //    // Configura el origen de datos del ListView u otro control con la lista de zonas
    //    // Por ejemplo, si deseas usar un ListView:
    //    zoneListView.ItemsSource = zones;
    //}

    //private async void OnSaveButtonClicked(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // Establece la cadena de conexi�n a tu base de datos
    //        string connectionString = "TuCadenaDeConexion";

    //        using (SqlConnection conexion = new SqlConnection(connectionString))
    //        {
    //            conexion.Open();

    //            // Define la consulta SQL para actualizar el registro en la base de datos
    //            string consultaSQL = "UPDATE Patrols SET PatrolDate = @PatrolDate, PersonName = @PersonName, ZoneName = @ZoneName, CampaignName = @CampaignName WHERE PatrolID = @PatrolID";

    //            using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
    //            {
    //                // Asigna los valores editados de los campos de entrada a los par�metros
    //                comando.Parameters.AddWithValue("@PatrolID", selectedPatrol.PatrolID);
    //                comando.Parameters.AddWithValue("@PatrolDate", DateTime.Parse(patrolDateEntry.Text));
    //                comando.Parameters.AddWithValue("@PersonName", personNameEntry.Text);
    //                comando.Parameters.AddWithValue("@ZoneName", zoneNameEntry.Text);
    //                comando.Parameters.AddWithValue("@CampaignName", campaignNameEntry.Text);

    //                // Ejecuta la consulta de actualizaci�n
    //                int filasActualizadas = comando.ExecuteNonQuery();

    //                if (filasActualizadas > 0)
    //                {
    //                    // Los datos se actualizaron correctamente
    //                    // Puedes mostrar un mensaje de �xito o realizar cualquier otra acci�n necesaria.
    //                    await DisplayAlert("�xito", "Los datos se actualizaron correctamente", "Aceptar");

    //                    // Regresa a la p�gina anterior
    //                    await Navigation.PopAsync();
    //                }
    //                else
    //                {
    //                    // No se pudo actualizar ning�n registro
    //                    await DisplayAlert("Error", "No se pudo actualizar ning�n registro", "Aceptar");
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Maneja las excepciones apropiadamente
    //        await DisplayAlert("Error", "Error al actualizar los datos: " + ex.Message, "Aceptar");
    //    }
    }
}