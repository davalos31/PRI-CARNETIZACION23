using PetPass.Model;
using PetPass.Service;
using PetPass.ViewModel;

namespace PetPass.View.Campaign;

public partial class DetailCampaign : ContentPage
{
    private CampaignViewModel viewModel;
    private CampaignService _campaignService;
    private int _id;
    public DetailCampaign()
    {
        InitializeComponent();
        // Inicializa viewModel con una instancia de CampaignViewModel y CampaignService
        viewModel = new CampaignViewModel(new CampaignService());
        _campaignService = new CampaignService();
        BindingContext = viewModel;
        LoadPersonData();
    }



    private async void LoadPersonData()
    {
        try
        {
            var campaignService = new CampaignService();
            List<Campaigns> campaignDataList = await campaignService.GetCampaignsAsync();

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
         
          
            await DisplayAlert("Error", "Ocurri� un error al cargar los datos de la campania.", "OK");
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
            DisplayAlert("Error", "No se ha seleccionado un elemento v�lido.", "OK");
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
            DisplayAlert("Error", "No se ha seleccionado un elemento v�lido.", "OK");
        }



        bool deleteConfirmation = _campaignService.DeleteCampaign(_id);

      

        bool result = await DisplayAlert("Confirmaci�n", "�Est� seguro de eliminar el registro?", "S�", "No");

        if (result && deleteConfirmation)
        {
           

            await DisplayAlert("�xito", "El registro se ha eliminado con �xito.", "OK");
        }
        else
        {
            // El usuario ha seleccionado "No", no se realiza ninguna acci�n.
        }
    }
}