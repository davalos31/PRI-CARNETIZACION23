using Microsoft.Extensions.Logging;
using PetPass.Model;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace PetPass.View.Patrol;

public partial class CreatePatrol : ContentPage
{
    private int _personID;
    private readonly string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass;User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";

    private List<Zone> allZones;
    private List<Zone> filteredZones;
    public ObservableCollection<Zone> Zones { get; set; }
    public ObservableCollection<Campaign> Campaigns { get; set; }

    private int selectedZoneID;
    private int selectedCampaignID;
    // Establece la fecha actual en el campo patrolDate
    private DateTime currentPatrolDate = DateTime.Now;
    public CreatePatrol(int personId)
	{
		InitializeComponent();
		_personID = personId;
      //  Zones = new ObservableCollection<Zone>(); // Inicializa la colección aquí
        Campaigns = new ObservableCollection<Campaign>();
        allZones = new List<Zone>();
        filteredZones = new List<Zone>();
        LoadDataFromDatabase();
        UpdateListView();
        BindingContext = this;
        LoadDataFromDatabase();
        LoadCampaignsFromDatabase();
       
    }

    private void LoadDataFromDatabase()
    {
        string consultaSQL = "SELECT TOP 10 zoneID, [name] FROM [Zone]";

        try
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        Zones = new ObservableCollection<Zone>(); // Inicializa la colección aquí

                        while (lector.Read())
                        {
                            byte zoneIDByte = lector.GetByte(0);
                            int zoneID = Convert.ToInt32(zoneIDByte);
                            string nombre = lector.GetString(1);
                            Zones.Add(new Zone { ZoneID = zoneID, Name = nombre });
                            Console.WriteLine($"zoneID: {zoneID}, Nombre: {nombre}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al cargar datos desde la base de datos1: " + ex.Message, "Aceptar");
        }

        UpdateListView(); // Llama a UpdateListView después de cargar los datos
    }



    private void UpdateListView()
    {
        zoneListView.ItemsSource = filteredZones; // Configura la ListView con filteredZones
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue;
        filteredZones = Zones.Where(zone => zone.Name.Contains(searchText)).ToList(); // Filtrar la lista Zones
        UpdateListView();
    }

    private void OnAddButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int zoneID)
        {
            // Almacena el ZoneID seleccionado en la variable selectedZoneID
            selectedZoneID = zoneID;

            // Realiza cualquier otra lógica que necesites aquí
        }
    }

    private void LoadCampaignsFromDatabase()
    {
        string consultaSQL = "SELECT campaignID, [name] FROM [Campaign] ";

        try
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                {
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                       
                        while (lector.Read())
                        {
                           
                            int campaignID = lector.GetInt32(0);
                           // int campaignID = Convert.ToInt32(campaignIDByte);
                            string nombre = lector.GetString(1);
                            Campaigns.Add(new Campaign { CampaignID = campaignID, Name = nombre });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al cargar datos de campañas desde la base de datos: " + ex.Message, "Aceptar");
        }
    }

    private void OnAddCampaignButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int campaignID)
        {
            // Almacena el CampaignID seleccionado en la variable selectedCampaignID
            selectedCampaignID = campaignID;

            // Realiza cualquier otra lógica que necesites aquí
        }
    }

    private void InsertPatrolIntoDatabase()
    {
        try
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                string insertSQL = "INSERT INTO [Patrol] ([patrolDate], [personID], [zoneID], [campaignID]) VALUES (@PatrolDate, @PersonID, @ZoneID, @CampaignID)";

                using (SqlCommand comando = new SqlCommand(insertSQL, conexion))
                {
                    // Configura los parámetros del comando
                    comando.Parameters.AddWithValue("@PatrolDate", currentPatrolDate); // Utiliza el valor de currentPatrolDate
                    comando.Parameters.AddWithValue("@PersonID", _personID);
                    comando.Parameters.AddWithValue("@ZoneID", selectedZoneID);
                    comando.Parameters.AddWithValue("@CampaignID", selectedCampaignID);

                    // Ejecuta el comando de inserción
                    comando.ExecuteNonQuery();
                }
            }

            DisplayAlert("Éxito", "Patrol insertado en la base de datos correctamente", "Aceptar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al insertar el patrol en la base de datos: " + ex.Message, "Aceptar");
        }
    }


    private void OnInsertButtonClicked(object sender, EventArgs e)
    {
        // Llama al método InsertPatrolIntoDatabase para insertar los datos en la base de datos
        InsertPatrolIntoDatabase();
    }



}


