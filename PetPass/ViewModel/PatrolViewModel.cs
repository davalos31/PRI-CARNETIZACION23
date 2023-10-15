using MvvmHelpers;
using PetPass.Model;
using PetPass.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetPass.ViewModel
{
    public class PatrolViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IPatrol _patrolService;
        private readonly int _personID;
        public ObservableCollection<Zone> Zones { get; set; }
        public ObservableCollection<Campaign> Campaigns { get; set; }

        private int _selectedZoneId;
        private int _selectedCampaignId;


        Person person;
        Zone zone;
        Campaign campaign;

        // Propiedades para las selecciones de zona y campaña
        public int SelectedZoneId
        {
            get { return _selectedZoneId; }
            set
            {
                if (_selectedZoneId != value)
                {
                    _selectedZoneId = value;
                    OnPropertyChanged(nameof(SelectedZoneId));
                    // Aquí puedes agregar cualquier lógica adicional relacionada con la selección de zona.
                }
            }
        }

        public int SelectedCampaignId
        {
            get { return _selectedCampaignId; }
            set
            {
                if (_selectedCampaignId != value)
                {
                    _selectedCampaignId = value;
                    OnPropertyChanged(nameof(SelectedCampaignId));

                }
            }
        }

        private DateTime _currentPatrolDate = DateTime.Now;
        public DateTime CurrentPatrolDate
        {
            get { return _currentPatrolDate; }
            set { SetProperty(ref _currentPatrolDate, value); }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand InsertPatrolCommand { get; }

        public event EventHandler<string> ShowMessageRequest; // Evento para mostrar mensajes

        public PatrolViewModel(int personID)
        {

            _personID = personID;
            _patrolService = new PatrolService();

            Zones = new ObservableCollection<Zone>();
            Campaigns = new ObservableCollection<Campaign>();

            LoadDataCommand = new Command(async () => await LoadDataAsync());
            InsertPatrolCommand = new Command(async () => await InsertPatrolAsync());
            InitializeDataAsync();

        }

        public async Task LoadDataAsync()
        {
            try
            {
                List<Zone> loadedZones = await _patrolService.GetZonesAsync();
                List<Campaign> loadedCampaigns = await _patrolService.GetCampaignsAsync();

                Zones.Clear();
                Campaigns.Clear();

                foreach (var zone in loadedZones)
                {
                    Zones.Add(zone);
                }

                foreach (var campaign in loadedCampaigns)
                {
                    Campaigns.Add(campaign);
                }

                // Agregar mensaje de depuración
                Console.WriteLine("Datos de zonas y campañas cargados correctamente.");
            }
            catch (Exception ex)
            {
                // Manejo de errores, log, etc.
                ShowMessageRequest?.Invoke(this, $"Error al cargar datos: {ex.Message}");
            }
        }

        public void SetSelectedZoneId(int zoneId)
        {
            SelectedZoneId = zoneId;
        }


        public void SetSelectedCampaignId(int campaignId)
        {
            SelectedCampaignId = campaignId;
        }




        private async Task InitializeDataAsync()
        {
            try
            {
                // Obtener los objetos person, zone y campaign de forma asíncrona
                person = await _patrolService.GetPersonByIdAsync(_personID);
                zone = await _patrolService.GetZoneByIdAsync(SelectedZoneId);
                campaign = await _patrolService.GetCampaignByIdAsync(SelectedCampaignId);
            }
            catch (Exception ex)
            {
                // Manejo de errores, log, etc.
                ShowMessageRequest?.Invoke(this, $"Error al cargar datos: {ex.Message}");
            }
        }



        public async Task InsertPatrolAsync()
        {
            try
            {
                // Obtener el objeto Zone correspondiente a partir del ID seleccionado
                Person person = await _patrolService.GetPersonByIdAsync(_personID);
                Zone zone = await _patrolService.GetZoneByIdAsync(SelectedZoneId);
                Campaign campaign = await _patrolService.GetCampaignByIdAsync(SelectedCampaignId);


                if (zone != null)
                {
                    // Crear una nueva patrulla y asignar valores
                    var patrol = new Patrol1
                    {
                        patrolDate = CurrentPatrolDate,
                        Person = person, // Asegúrate de que person esté inicializado correctamente
                        Zone = zone,     // Asignar el objeto Zone en lugar de un entero
                        Campaign = campaign // Asegúrate de que campaign esté inicializado correctamente
                    };

                    var createdPatrol = await _patrolService.CreatePatrolAsync(patrol);

                    if (createdPatrol != null)
                    {
                        // Éxito: mostrar un mensaje
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Patrulla creada exitosamente.", "Aceptar");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Error al crear la patrulla.", "Aceptar");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La zona seleccionada no es válida.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al insertar la patrulla: {ex.Message}", "Aceptar");
            }
        }
    }
}
