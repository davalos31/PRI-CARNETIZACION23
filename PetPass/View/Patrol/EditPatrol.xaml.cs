
using PetPass.Model;
using PetPass.Service;
using PetPass.ViewModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace PetPass.View.Patrol;

public partial class EditPatrol : ContentPage
{
  //  private PatrolViewModel _viewModel;
    private PatrolService _patrolService;
    private ObservableCollection<string> zoneNames = new ObservableCollection<string>();
    private ObservableCollection<string> campaignNames = new ObservableCollection<string>();
    private List<Zone> zones;
    private List<Campaigns> campaigns;
    private int selectedZoneId;
    private int selectedCampaignId;
    private DateTime selectedDate;
    int _idPatrolValue;
    string _tokenValue;
    int _idUser;
    public EditPatrol(int _idPatrol, string _token,int _userID)
	{

        InitializeComponent();

        _patrolService = new PatrolService();
      //  _viewModel = new PatrolViewModel(_idPatrol);
      //  BindingContext = _viewModel;
        _idUser = _userID;
        zonePicker.ItemsSource = zoneNames;
        campaignPicker.ItemsSource = campaignNames;

        zones = new List<Zone>();
        campaigns = new List<Campaigns>();

        _idPatrolValue = _idPatrol;
        _tokenValue = _token;

        LoadPatrolDetails(_idPatrolValue);
        LoadData();
    }



    private async void LoadPatrolDetails(int Id)
    {
        var patrol = await _patrolService.GetPatrolDetailsAsyncApi(_tokenValue, _idPatrolValue);

        if (patrol != null)
        {
            if (patrol.Zone != null)
            {
                zonePicker.SelectedItem = patrol.Zone.Name;
            }

            if (patrol.Campaign != null)
            {
                campaignPicker.SelectedItem = patrol.Campaign.Name;
            }

            patrolDatePicker.Date = patrol.PatrolDate;
        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar los detalles de la Persona.", "OK");

        }
    }


    private async void UpdatePatrolButton_Clicked(object sender, EventArgs e)
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
                PersonId = 3,
                ZoneId = 1,
                CampaignId = 1
            };

            // Llama al método CreatePersonAsync de PersonService
            var updatePatrol = await _patrolService.UpdatePatrolApi(newPatrol);

            if (updatePatrol != null)
            {
                DisplayAlert("Éxito", "Jefe Brigada Actualizado correctamente.", "OK");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "No se pudo actualizar el Jefe Brigada.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

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
}


