using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Logging;
using PetPass.Model;
using PetPass.Service;
using PetPass.ViewModel;


namespace PetPass.View.Patrol
{
    public partial class CreatePatrol : ContentPage
    {
        private PatrolViewModel _viewModel;
        private PatrolService _patrolService;
        private ObservableCollection<string> zoneNames = new ObservableCollection<string>();
        private ObservableCollection<string> campaignNames = new ObservableCollection<string>();
        private List<Zone> zones;
        private List<Campaigns> campaigns;
        private int selectedZoneId;
        private int selectedCampaignId;
        string _tokenValue;
        int _idUser;
        private DateTime selectedDate;

        public CreatePatrol(int personID, string _token)
        {
            InitializeComponent();
            _patrolService = new PatrolService();
            _viewModel = new PatrolViewModel(personID);
            BindingContext = _viewModel;

            zonePicker.ItemsSource = zoneNames;
            campaignPicker.ItemsSource = campaignNames;

            zones = new List<Zone>();
            campaigns = new List<Campaigns>();

            _tokenValue = _token;
            _idUser = personID;

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                PatrolService _patrolService = new PatrolService();
                zones = await _patrolService.GetZonesAsyncApi(_tokenValue);
                campaigns = await _patrolService.GetCampaignsAsync();

                zoneNames.Clear();
                campaignNames.Clear();

                foreach (var zone in zones)
                {
                    zoneNames.Add(zone.Name);
                }

                foreach (var campaign in campaigns)
                {
                    campaignNames.Add(campaign.Name);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error al cargar lista de datos", ex.Message, "OK");
            }
        }

        private void OnZonePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedZoneName = (string)zonePicker.SelectedItem;
            var selectedZone = zones.FirstOrDefault(z => z.Name == selectedZoneName);

            if (selectedZone != null)
            {
                selectedZoneId = selectedZone.ZoneID;
               
            }
        }

        private void OnCampaignPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCampaignName = (string)campaignPicker.SelectedItem;
            var selectedCampaign = campaigns.FirstOrDefault(c => c.Name == selectedCampaignName);

            if (selectedCampaign != null)
            {
                selectedCampaignId = selectedCampaign.CampaignID;
                
            }
        }

        private async void OnInsertButtonClicked(object sender, EventArgs e)
        {
            try
            {
                selectedDate = patrolDatePicker.Date;
       

               

                string authToken = _tokenValue;


                Persons person = null;
                Zone zone = null;
                Campaigns campaign = null;

                person = new Persons { PersonId = _idUser };
                byte zoneId = (byte)selectedZoneId; // Realiza una conversión explícita a byte
                campaign = new Campaigns { CampaignID = selectedCampaignId };

                Patrol1 newPatrol = new Patrol1
                {
                    PatrolDate = DateTime.Now,
                    PersonId = _idUser,
                    ZoneId = zoneId, 
                    CampaignId = selectedCampaignId
                };

                bool createResult = await _patrolService.CreatePatrolAsyncApi(authToken, newPatrol);

                    if (createResult)
                    {
                        await DisplayAlert("Éxito", "Patrulla creada", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo crear la patrulla", "OK");
                    }
                

              
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
        }

       
    }
}
