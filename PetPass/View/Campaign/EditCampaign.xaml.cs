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
            // Asigna los detalles de la campaña a los controles del formulario de edición.
            CampaignNameEntry.Text = campaign.Name;
            StartDatePicker.Date = campaign.StartDate;
            EndDatePicker.Date = campaign.EndDate;

        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar los detalles de la campaña.", "OK");
            // Maneja el caso en el que no se encontraron detalles de la campaña.
        }
    }

    private void UpdateCampaignButton_Clicked(object sender, EventArgs e)
    {
        int campaignIDToUpdate = idCampaign; // Obtén el ID de la campaña que deseas actualizar
        string newCampaignName = CampaignNameEntry.Text; // Obtén el nuevo nombre de la campaña desde el formulario
        DateTime newStartDate = StartDatePicker.Date; // Obtén la nueva fecha de inicio desde el formulario
        DateTime newEndDate = EndDatePicker.Date; // Obtén la nueva fecha de finalización desde el formulario


        // Llama al método para actualizar la campaña con los datos obtenidos
        //bool updated = _campaignService.UpdateCampaign(campaignIDToUpdate, newCampaignName, newStartDate, newEndDate);
        bool updated = true;

        if (updated)
        {
            DisplayAlert("Éxito", "La campaña se actualizó correctamente.", "OK");
          //  Navigation.PushAsync(new DetailCampaign());
            // Puedes manejar la navegación o cualquier otra acción aquí
        }
        else
        {
            DisplayAlert("Error", "No se pudo actualizar la campaña.", "OK");
        }
    }

    private void Back(object sender, EventArgs e)
    {
       // Navigation.PushAsync(new DetailCampaign());
    }

}