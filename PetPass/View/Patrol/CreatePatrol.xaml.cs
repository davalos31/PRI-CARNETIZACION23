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
        private List<Campaign> campaigns;
        private int selectedZoneId;
        private int selectedCampaignId;

        public CreatePatrol(int personID)
        {
            InitializeComponent();
            _patrolService = new PatrolService();
            _viewModel = new PatrolViewModel(personID);
            BindingContext = _viewModel;

            zonePicker.ItemsSource = zoneNames;
            campaignPicker.ItemsSource = campaignNames;

            zones = new List<Zone>();
            campaigns = new List<Campaign>();

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                zones = await _patrolService.GetZonesAsync();
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
                await DisplayAlert("Error al cargar listas", ex.Message, "OK");
            }
        }

        private void OnZonePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedZoneName = (string)zonePicker.SelectedItem;
            var selectedZone = zones.FirstOrDefault(z => z.Name == selectedZoneName);

            if (selectedZone != null)
            {
                selectedZoneId = selectedZone.ZoneID;
                _viewModel.SetSelectedZoneId(selectedZoneId);
            }
        }

        private void OnCampaignPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCampaignName = (string)campaignPicker.SelectedItem;
            var selectedCampaign = campaigns.FirstOrDefault(c => c.Name == selectedCampaignName);

            if (selectedCampaign != null)
            {
                selectedCampaignId = selectedCampaign.CampaignID;
                _viewModel.SetSelectedCampaignId(selectedCampaignId);
            }
        }

        private async void OnInsertButtonClicked(object sender, EventArgs e)
        {
            await _viewModel.InsertPatrolAsync();
        }

       
    }
}
