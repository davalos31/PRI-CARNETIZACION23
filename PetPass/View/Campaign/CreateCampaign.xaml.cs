using PetPass.Service;
using PetPass.ViewModel;

namespace PetPass.View.Campaign;

public partial class CreateCampaign : ContentPage
{
	public CreateCampaign()
	{
		InitializeComponent();
        BindingContext = new CampaignViewModel(new CampaignService());
    }
}