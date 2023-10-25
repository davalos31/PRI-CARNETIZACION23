using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Microsoft.Maui.Controls;
using PetPass.Model;
using System.Collections.Generic;
using PetPass.Service;


namespace PetPass.View.Patrol
{
    public partial class DetailPatrol : ContentPage
    {
        private ObservableCollection<Patrol1> Patrols;
        private string _connectionString = "Server=DbPetPass.mssql.somee.com; Database=DbPetPass; User=nahuubj_SQLLogin_1; Password=z5qp9mphxt; Trusted_Connection=false; Encrypt=False;";
        private PatrolService _patrolService;
        string _tokenValue;

        public DetailPatrol(string _tokenvalue)
        {
            InitializeComponent();
            _tokenValue = _tokenvalue;
            _patrolService = new PatrolService();
      
            LoadDateApiAsync();
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
                            Patrols = new ObservableCollection<Patrol1>(); // Inicializa la colecci�n aqu�

                            while (lector.Read())
                            {
                                int patrolID = lector.GetInt32(0);
                                DateTime patrolDate = lector.GetDateTime(1);
                                int personID = lector.GetInt32(2);
                                byte zoneID = lector.GetByte(3);
                                int campaignID = lector.GetInt32(4);

                                // Obt�n los datos relacionados de las tablas Person, Zone y Campaign
                                Persons person = GetPersonById(personID);
                                Zone zone = GetZoneById(zoneID);
                                Campaigns campaign = GetCampaignById(campaignID);

                                Patrols.Add(new Patrol1
                                {
                                    PatrolId= patrolID,
                                    PatrolDate = patrolDate,
                                 //   Person = person.PersonId,
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

            UpdateListView(); // Llama a la funci�n para actualizar la vista con los datos cargados
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            PatrolListView.ItemsSource = Patrols;
        }


        private async Task LoadDateApiAsync()
        {
            try
            {
                // Llama al m�todo para obtener la lista de patrullas, pasando el token de autorizaci�n
                Patrols = new ObservableCollection<Patrol1>();
                string authToken = _tokenValue; // Reemplaza con tu token real
                List<Patrol1> patrolList = await _patrolService.GetPatrolAsyncApi(authToken);
               
                if (patrolList != null)
                {
                    // Los datos se cargaron con �xito, ahora puedes usar la lista `patrolList`.
                    foreach (var patrol in patrolList)
                    {
                        Patrols.Add(patrol);
                    }
                }
                else
                {
                    // Maneja el caso en el que no se pudieron obtener datos (error o lista vac�a).
                }
            }
            catch (Exception ex)
            {
                // Maneja excepciones, como problemas de red o errores en la solicitud HTTP.
                Console.WriteLine("Error al cargar datos: " + ex.Message);
            }
        }



        private void Edit(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int patrolId)
            {
                string userIDValue = patrolId.ToString();
                DisplayAlert("ID Guardado", "El valor de la patrulla es: " + userIDValue, "OK");
                Navigation.PushAsync(new EditPatrol(patrolId, _tokenValue));
            }
            else
            {
                DisplayAlert("Error", "No se ha seleccionado un elemento v�lido.", "OK");
            }

        }










        private void UpdateListView()
        {
            // Asigna la colecci�n Patrols como origen de datos para tu ListView
            //zoneListView.ItemsSource = Patrols;
        }

        private Persons GetPersonById(int personID)
        {
            Persons person = null;

            // Establece la cadena de conexi�n a tu base de datos
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
                                person = new Persons
                                {
                                    PersonId = personId,
                                    Name = name
                                    // Puedes agregar m�s propiedades seg�n tu tabla Person
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

            // Establece la cadena de conexi�n a tu base de datos
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
                                    // Puedes agregar m�s propiedades seg�n tu tabla Zone
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

        private Campaigns GetCampaignById(int campaignID)
        {
            Campaigns campaign = null;

            // Establece la cadena de conexi�n a tu base de datos
            string connectionString = _connectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Define la consulta SQL para obtener la campa�a por ID
                    string consultaSQL = "SELECT campaignID, name FROM Campaign WHERE campaignID = @CampaignID";

                    using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                    {
                        comando.Parameters.AddWithValue("@CampaignID", campaignID);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                // Recupera los datos de la campa�a
                                int campaignId = lector.GetInt32(0);
                                string campaignName = lector.GetString(1);

                                // Crea una instancia de Campaign con los datos recuperados
                                campaign = new Campaigns
                                {
                                    CampaignID = campaignId,
                                    Name = campaignName
                                    // Puedes agregar m�s propiedades seg�n tu tabla Campaign
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones apropiadamente
                Console.WriteLine("Error al obtener datos de la campa�a: " + ex.Message);
            }

            return campaign;
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var selectedPatrol = (Patrol1)((Button)sender).CommandParameter;

            // Navega a la p�gina de edici�n y pasa los datos seleccionados como par�metro
          //  await Navigation.PushAsync(new EditPatrol(selectedPatrol));
        }
    }
}