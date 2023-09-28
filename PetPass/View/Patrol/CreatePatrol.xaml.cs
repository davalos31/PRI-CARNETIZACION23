using PetPass.Model;
using System.Data.SqlClient;

namespace PetPass.View.Patrol;

public partial class CreatePatrol : ContentPage
{
    private int _personID;
    private readonly string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";
    public CreatePatrol(int personId)
	{
		InitializeComponent();
		_personID = personId;
     
        CargarDatosParaPickers();


    }


    private async void CargarDatosParaPickers()
    {
        try
        {
            // Llena el Picker de Zone
            List<Zone> zones = await ObtenerZonasAsync();
            foreach (Zone zone in zones)
            {
                ZonePicker.Items.Add(zone.Name);
            }

            // Llena el Picker de Campaign
            //List<Campaign> campaigns = await ObtenerCampañasAsync();
            //foreach (Campaign campaign in campaigns)
            //{
            //    CampaignPicker.Items.Add(campaign.Name);
            //}
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar datos para los pickers: {ex.Message}", "Aceptar");
        }
    }


    private async Task<List<Zone>> ObtenerZonasAsync()
    {
        // Realiza una consulta SQL para obtener datos de la tabla Zone
        List<Zone> zones = new List<Zone>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SELECT [name] FROM [DbPetPass].[dbo].[Zone]", connection))
            {
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    zones.Add(new Zone { Name = name });
                }
            }
        }
        return zones;
    }

    //private async Task<List<Campaign>> ObtenerCampañasAsync()
    //{
    //    // Realiza una consulta SQL para obtener datos de la tabla Campaign
    //    List<Campaign> campaigns = new List<Campaign>();
    //    using (SqlConnection connection = new SqlConnection(_connectionString))
    //    {
    //        await connection.OpenAsync();
    //        using (SqlCommand command = new SqlCommand("SELECT [campaignID], [name] FROM [DbPetPass].[dbo].[Campaign]", connection))
    //        {
    //            SqlDataReader reader = await command.ExecuteReaderAsync();
    //            while (reader.Read())
    //            {
    //                int campaignID;
    //                object campaignIDObject = reader[0];
    //                if (campaignIDObject != DBNull.Value)
    //                {
    //                    campaignID = Convert.ToInt32(campaignIDObject);
    //                }
    //                else
    //                {
    //                    campaignID = -1; // Otra acción por defecto si es DBNull
    //                }

    //                string name = reader.GetString(1);
    //                campaigns.Add(new Campaign { CampaignID = campaignID, Name = name });
    //            }
    //        }
    //    }
    //    return campaigns;
    //}

    // Evento para guardar la patrulla con los valores seleccionados
    private async void GuardarButton_Clicked(object sender, EventArgs e)
    {
        // Obtén la fecha seleccionada en el DatePicker
        DateTime selectedDate = DatePicker.Date;

        // Obtén los valores seleccionados en los pickers
        string selectedZoneName = ZonePicker.SelectedItem as string;
        string selectedCampaignName = CampaignPicker.SelectedItem as string;

        // Busca el ID correspondiente a los valores seleccionados
        int selectedZoneID = await ObtenerIDDeZonaAsync(selectedZoneName);
        int selectedCampaignID = await ObtenerIDDeCampañaAsync(selectedCampaignName);

        // Ahora tienes los IDs de Zone y Campaign, así como la fecha seleccionada.
        // Puedes usarlos para realizar el insert en la tabla Patrol.
        await InsertarPatrullaAsync(selectedZoneID, selectedCampaignID, selectedDate);
    }

    private async Task<int> ObtenerIDDeZonaAsync(string zoneName)
    {
        // Realiza una consulta SQL para obtener el ID de la zona
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(
                "SELECT [zoneID] FROM [DbPetPass].[dbo].[Zone] WHERE [name] = @ZoneName", connection))
            {
                command.Parameters.AddWithValue("@ZoneName", zoneName);
                object result = await command.ExecuteScalarAsync();
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }
            }
        }
        return -1; // Valor predeterminado en caso de error
    }

    private async Task<int> ObtenerIDDeCampañaAsync(string campaignName)
    {
        // Realiza una consulta SQL para obtener el ID de la campaña
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(
                "SELECT [campaignID] FROM [DbPetPass].[dbo].[Campaign] WHERE [name] = @CampaignName", connection))
            {
                command.Parameters.AddWithValue("@CampaignName", campaignName);
                object result = await command.ExecuteScalarAsync();
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }
            }
        }
        return -1; // Valor predeterminado en caso de error
    }

    private async Task InsertarPatrullaAsync(int selectedZoneID, int selectedCampaignID, DateTime selectedDate)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO [DbPetPass].[dbo].[Patrol] ([patrolDate], [personID], [zoneID], [campaignID]) " +
                    "VALUES (@PatrolDate, @PersonID, @ZoneID, @CampaignID)", connection))
                {
                    command.Parameters.AddWithValue("@PatrolDate", selectedDate); // Agrega la fecha de patrullaje
                    command.Parameters.AddWithValue("@PersonID", _personID);
                    command.Parameters.AddWithValue("@ZoneID", selectedZoneID);
                    command.Parameters.AddWithValue("@CampaignID", selectedCampaignID);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Si rowsAffected es mayor que 0, la inserción fue exitosa.
                    if (rowsAffected > 0)
                    {
                        await DisplayAlert("Éxito", "La patrulla se creó exitosamente.", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un error al crear la patrulla.", "Aceptar");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la inserción.
            Console.WriteLine($"Error al insertar patrulla: {ex.Message}");
        }
    }
}


