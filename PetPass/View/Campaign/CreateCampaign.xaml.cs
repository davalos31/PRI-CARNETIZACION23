using PetPass.Model;
using PetPass.Service;

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

        try
        {
            var campaign = new Campaigns
            {
                Name = nameCam,
                StartDate = startDate,
                EndDate = endDate, 
            };

            var result = await campaignService.CreateCampaignAsync(campaign,_token);

            if (result != null)
            {
                // �xito: la campa�a se cre� con �xito
                await Application.Current.MainPage.DisplayAlert("�xito", "La campa�a se cre� con �xito.", "OK");
                 Clear();
                Navigation.PopAsync();
            }
            else
            {
                // Error: no se pudo crear la campa�a
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la campa�a.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurri� un error: " + ex.Message, "OK");
        }
    }

    void Clear()
    {
        NameEntry.Text = "";
        EndDateEntry.Date = DateTime.Now;
    }
}