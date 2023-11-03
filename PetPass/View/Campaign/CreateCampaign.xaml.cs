using PetPass.Model;
using PetPass.Service;
using PetPass.Validation;

namespace PetPass.View.Campaign;

public partial class CreateCampaign : ContentPage
{
    string _token;
    private readonly CampaignService campaignService;
    public CreateCampaign(string token)
    {
        InitializeComponent();
        campaignService = new CampaignService();
        _token = token;

    }


    private async void OnSaveCampaignClicked(object sender, EventArgs e)
    {
        await SaveCampaignAsync();
    }



    private async Task SaveCampaignAsync()
    {

        string nameCam = NameEntry.Text;
        DateTime startDate = DateTime.Now;
        DateTime endDate = EndDateEntry.Date;
        Validations val = new Validations();
        (bool isNameValid, string nameError) = val.ValidateCampaignName(nameCam);
        (bool isEndDateValid, string EndDateError) = val.ValidateEndDate(endDate);
        (bool isCharacterValid, string CharacterError) = val.ContainsSpecialCharacters(nameCam);

        if (isNameValid && isEndDateValid && isCharacterValid)
        {
            try
            {
                var campaign = new Campaigns
                {
                    Name = nameCam,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                var result = await campaignService.CreateCampaignAsync(campaign, _token);

                if (result != null)
                {
                    // Éxito: la campaña se creó con éxito
                    await Application.Current.MainPage.DisplayAlert("Éxito", "La campaña se creó con éxito.", "OK");
                    Clear();
                    Navigation.PopAsync();
                }
                else
                {
                    // Error: no se pudo crear la campaña
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la campaña.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
            }
        }
        else
        {
            string errorMessage = "Por favor, corrija los siguientes errores:\n";
            if (!isNameValid) errorMessage += $"- {nameError}\n";
            if (!isEndDateValid) errorMessage += $"- {EndDateError}\n";
            if (!isCharacterValid) errorMessage += $"- {CharacterError}\n";

            await DisplayAlert("Error", errorMessage, "OK");
        }

    }

    void Clear()
    {
        NameEntry.Text = "";
        EndDateEntry.Date = DateTime.Now;
    }
}