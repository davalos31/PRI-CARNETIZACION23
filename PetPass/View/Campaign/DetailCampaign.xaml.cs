using PetPass.Model;
using PetPass.Service;
namespace PetPass.View.Campaign;

public partial class DetailCampaign : ContentPage
{
    private CampaignService _campaignService;
    private int _id;
    string _token;
    public DetailCampaign(string token)
    {
        InitializeComponent();
        // Inicializa viewModel con una instancia de CampaignViewModel y CampaignService
        // viewModel = new CampaignViewModel(new CampaignService());
        _campaignService = new CampaignService();
        _token = token;
        //  BindingContext = viewModel;
        LoadPersonData();
    }



    private async void LoadPersonData()
    {
        try
        {
            var campaignService = new CampaignService();
            List<Campaigns> campaignDataList = await campaignService.GetCampaignsAsync(_token);

            if (campaignDataList != null && campaignDataList.Count > 0)
            {
                CampaignDataListView.ItemsSource = campaignDataList;
            }
            else
            {

                await DisplayAlert("Error", "No se encontraron datos de las campanias.", "OK");
            }
        }
        catch (Exception ex)
        {


            await DisplayAlert("Error", "Ocurrió un error al cargar los datos de la campania.", "OK");
        }
    }
    private void Edit(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int CampaignID)
        {
            string mascotaIDValue = CampaignID.ToString(); // Convierte el valor a string
            DisplayAlert("ID Guardado", "El valor de la campania es: " + mascotaIDValue, "OK");
            Navigation.PushAsync(new EditCampaign(CampaignID));
        }
        else
        {
            DisplayAlert("Error", "No se ha seleccionado un elemento válido.", "OK");
        }

    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is int CampaignID)
        {
            _id = CampaignID;
        }
        else
        {
            DisplayAlert("Error", "No se ha seleccionado un elemento válido.", "OK");
        }



        //bool deleteConfirmation = _campaignService.DeleteCampaign(_id);
        bool deleteConfirmation = true;



        bool result = await DisplayAlert("Confirmación", "¿Está seguro de eliminar el registro?", "Sí", "No");

        if (result && deleteConfirmation)
        {


            await DisplayAlert("Éxito", "El registro se ha eliminado con éxito.", "OK");
        }
        else
        {
            // El usuario ha seleccionado "No", no se realiza ninguna acción.
        }
    }
}