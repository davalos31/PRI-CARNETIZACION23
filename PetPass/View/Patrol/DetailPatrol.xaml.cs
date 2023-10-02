using PetPass.Model;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PetPass.View.Patrol;

public partial class DetailPatrol : ContentPage
{

    private ObservableCollection<Patrol1> Patrols;
    private string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";
    public DetailPatrol()
	{
		InitializeComponent();
        LoadDataFromDatabase();

    }
    private void LoadDataFromDatabase()
    {
        string consultaSQL = "SELECT TOP 10 patrolID, patrolDate, personID, zoneID, campaignID FROM Patrol";

        try
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        Patrols = new ObservableCollection<Patrol1>(); // Inicializa la colección aquí

                        while (lector.Read())
                        {
                            int patrolID = lector.GetInt32(0);
                            DateTime patrolDate = lector.GetDateTime(1);
                            int personID = lector.GetInt32(2);
                            byte zoneID = lector.GetByte(3);
                            int campaignID = lector.GetInt32(4);

                            // Obtén los datos relacionados de las tablas Person, Zone y Campaign
                            Person person = GetPersonById(personID);
                            Zone zone = GetZoneById(zoneID);
                            Campaign campaign = GetCampaignById(campaignID);

                            Patrols.Add(new Patrol1
                            {
                                patrolID = patrolID,
                                patrolDate = patrolDate,
                                Person = person,
                                Zone = zone,
                                Campaign = campaign
                            });

                            Console.WriteLine($"PatrolID: {patrolID}, Date: {patrolDate}, PersonID: {personID}, ZoneID: {zoneID}, CampaignID: {campaignID}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al cargar datos desde la base de datos: " + ex.Message, "Aceptar");
        }

        UpdateListView(); // Llama a la función para actualizar la vista con los datos cargados
    }

    private void UpdateListView()
    {
        // Asigna la colección Patrols como origen de datos para tu ListView
        zoneListView.ItemsSource = Patrols;
    }

    ////
    ///
    private Person GetPersonById(int personID)
    {
        Person person = null;

        // Establece la cadena de conexión a tu base de datos
        string connectionString = _connectionString;

        try
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                // Define la consulta SQL para obtener la persona por ID
                string consultaSQL = "SELECT personID, name FROM Person WHERE personID = @PersonID";

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@PersonID", personID);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            // Recupera los datos de la persona
                            int personId = lector.GetInt32(0);
                            string name = lector.GetString(1);

                            // Crea una instancia de Person con los datos recuperados
                            person = new Person
                            {
                                PersonId = personId,
                                Name = name
                                // Puedes agregar más propiedades según tu tabla Person
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Maneja las excepciones apropiadamente
            Console.WriteLine("Error al obtener datos de la persona: " + ex.Message);
        }

        return person;
    }

    private Zone GetZoneById(byte zoneID)
    {
        Zone zone = null;

        // Establece la cadena de conexión a tu base de datos
        string connectionString = _connectionString;

        try
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                // Define la consulta SQL para obtener la zona por ID
                string consultaSQL = "SELECT zoneID, name FROM Zone WHERE zoneID = @ZoneID";

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@ZoneID", zoneID);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            // Recupera los datos de la zona
                            byte zoneId = lector.GetByte(0);
                            string name = lector.GetString(1);

                            // Crea una instancia de Zone con los datos recuperados
                            zone = new Zone
                            {
                                ZoneID = zoneId,
                                Name = name
                                // Puedes agregar más propiedades según tu tabla Zone
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Maneja las excepciones apropiadamente
            Console.WriteLine("Error al obtener datos de la zona: " + ex.Message);
        }

        return zone;
    }

    private Campaign GetCampaignById(int campaignID)
    {
        Campaign campaign = null;

        // Establece la cadena de conexión a tu base de datos
        string connectionString = _connectionString;

        try
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                // Define la consulta SQL para obtener la campaña por ID
                string consultaSQL = "SELECT campaignID, name FROM Campaign WHERE campaignID = @CampaignID";

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    comando.Parameters.AddWithValue("@CampaignID", campaignID);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            // Recupera los datos de la campaña
                            int campaignId = lector.GetInt32(0);
                            string campaignName = lector.GetString(1);

                            // Crea una instancia de Campaign con los datos recuperados
                            campaign = new Campaign
                            {
                                CampaignID = campaignId,
                                Name = campaignName
                                // Puedes agregar más propiedades según tu tabla Campaign
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Maneja las excepciones apropiadamente
            Console.WriteLine("Error al obtener datos de la campaña: " + ex.Message);
        }

        return campaign;
    }

    private async void OnEditButtonClicked(object sender, EventArgs e)
    {
        var selectedPatrol = (Patrol1)((Button)sender).CommandParameter;

        // Navega a la página de edición y pasa los datos seleccionados como parámetro
        await Navigation.PushAsync(new EditPatrol(selectedPatrol));
    }





}
