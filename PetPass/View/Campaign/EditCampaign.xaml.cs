using PetPass.Service;

namespace PetPass.View.Campaign;

public partial class EditCampaign : ContentPage
{

    private CampaignService _campaignService;
    int idCampaign;
    public EditCampaign(int id)
	{
		InitializeComponent();
        _campaignService = new CampaignService();
        idCampaign = id;
        LoadCampaignDetails(id);

    }

    private async void LoadCampaignDetails(int campaignId)
    {
        var campaign = await _campaignService.GetCampaignAsync(campaignId);

        if (campaign != null)
        {
            // Asigna los detalles de la campa�a a los controles del formulario de edici�n.
            CampaignNameEntry.Text = campaign.Name;
            StartDatePicker.Date = campaign.StartDate;
            EndDatePicker.Date = campaign.EndDate;

        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar los detalles de la campa�a.", "OK");
            // Maneja el caso en el que no se encontraron detalles de la campa�a.
        }
    }

    private void UpdateCampaignButton_Clicked(object sender, EventArgs e)
    {
        int campaignIDToUpdate = idCampaign; // Obt�n el ID de la campa�a que deseas actualizar
        string newCampaignName = CampaignNameEntry.Text; // Obt�n el nuevo nombre de la campa�a desde el formulario
        DateTime newStartDate = StartDatePicker.Date; // Obt�n la nueva fecha de inicio desde el formulario
        DateTime newEndDate = EndDatePicker.Date; // Obt�n la nueva fecha de finalizaci�n desde el formulario


        // Llama al m�todo para actualizar la campa�a con los datos obtenidos
        //bool updated = _campaignService.UpdateCampaign(campaignIDToUpdate, newCampaignName, newStartDate, newEndDate);
        bool updated = true;

        if (updated)
        {
            DisplayAlert("�xito", "La campa�a se actualiz� correctamente.", "OK");
          //  Navigation.PushAsync(new DetailCampaign());
            // Puedes manejar la navegaci�n o cualquier otra acci�n aqu�
        }
        else
        {
            DisplayAlert("Error", "No se pudo actualizar la campa�a.", "OK");
        }
    }

    private void Back(object sender, EventArgs e)
    {
       // Navigation.PushAsync(new DetailCampaign());
    }

}