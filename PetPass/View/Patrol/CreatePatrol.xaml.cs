using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Logging;
using PetPass.Model;
using PetPass.Service;
using PetPass.Validation;

namespace PetPass.View.Patrol
{
    public partial class CreatePatrol : ContentPage
    {
        // private PatrolViewModel _viewModel;
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
            _tokenValue = _token;
            _idUser = personID;

            // Inicializa las listas aquí
            zones = new List<Zone>();
            campaigns = new List<Campaigns>();

            zonePicker.ItemsSource = zoneNames;
            campaignPicker.ItemsSource = campaignNames;

            LoadData(); // Llama al método para cargar los datos
        }

        private async void LoadData()
        {
            try
            {
                PatrolService _patrolService = new PatrolService();
                zones = await _patrolService.GetZonesAsyncApi(_tokenValue);
                campaigns = await _patrolService.GetCampaignsAsync(_tokenValue);

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
                // Handle the exception, for example, log it or display an error message.
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
                // Deshabilitar el botón antes de iniciar el proceso
                CreatePatrolButton.IsEnabled = false;

                selectedDate = patrolDatePicker.Date;
                string authToken = _tokenValue;

                Model.Person person = null;
                Zone zone = null;
                Campaigns campaign = null;

                person = new Model.Person() { PersonId = _idUser };
                byte zoneId = (byte)selectedZoneId;
                campaign = new Campaigns { CampaignID = selectedCampaignId };

                Patrol1 newPatrol = new Patrol1
                {
                    PatrolDate = selectedDate,
                    PersonId = _idUser,
                    ZoneId = zoneId,
                    CampaignId = selectedCampaignId
                };

                Validations val = new Validations();

                // Validaciones
                (bool isNameValid, string nameError) = val.ValidateFields(zoneId, selectedCampaignId);
                (bool isEndDateValid, string EndDateError) = val.ValidateDate(selectedDate);

                if (isNameValid && isEndDateValid && selectedDate > DateTime.Now)
                {
                    // Validación adicional para verificar que la zona y la campaña no estén vacías
                    if (zoneId == 0 || selectedCampaignId == 0)
                    {
                        await DisplayAlert("Error", "La zona y la campaña no pueden estar vacías.", "OK");
                        return;
                    }

                    bool createResult = await _patrolService.CreatePatrolAsyncApi(authToken, newPatrol);

                    if (createResult)
                    {
                        await DisplayAlert("Éxito", "Patrulla creada", "OK");
                        Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo crear la patrulla", "OK");
                    }
                }
                else
                {
                    string errorMessage = "Por favor, corrija los siguientes errores:\n";
                    if (!isNameValid) errorMessage += $"- {nameError}\n";
                    if (!isEndDateValid) errorMessage += $"- {EndDateError}\n";

                    // Validación adicional para verificar que la zona y la campaña no estén vacías
                    if (zoneId == 0 || selectedCampaignId == 0)
                    {
                        errorMessage += "- La zona y la campaña no pueden estar vacías.\n";
                    }

                    // Validación adicional para verificar que la fecha sea posterior a la actual
                    if (selectedDate <= DateTime.Now)
                    {
                        errorMessage += "- La fecha debe ser posterior a la actual.\n";
                    }

                    await DisplayAlert("Error", errorMessage, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
            finally
            {
                // Habilitar el botón después de completar el proceso
                CreatePatrolButton.IsEnabled = true;
            }
        }


    }
}
