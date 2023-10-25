using MvvmHelpers;
using PetPass.Model;
using PetPass.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.ViewModel
{
    
    public class CampaignViewModel: BaseViewModel, INotifyPropertyChanged
    {
        //private readonly ICampaign campaignService;


        public CampaignViewModel(ICampaign campaignService)
        {
            //this.campaignService = campaignService;
            //SaveCampaignCommand = new Command(async () => await SaveCampaignAsync());
            //LoadCampaignsAsync();
        }

        //private List<Campaigns> _campaign;
        //public List<Campaigns> Campaign
        //{
        //    get => _campaign;
        //    set
        //    {
        //        if (_campaign != value)
        //        {
        //            _campaign = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //// Otros miembros de tu ViewModel

        //public async Task LoadCampaignsAsync()
        //{
        //    try
        //    {
        //       // List<Campaigns> campaignList = await campaignService.GetCampaignsAsync();
        //       // Campaign = campaignList;

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al cargar las campañas: " + ex.Message);
        //    }
        //}




        //private string campaignName;
        //public string CampaignName
        //{
        //    get => campaignName;
        //    set
        //    {
        //        campaignName = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private DateTime startDate;
        //public DateTime StartDate
        //{
        //    get => startDate;
        //    set
        //    {
        //        startDate = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private DateTime endDate;
        //public DateTime EndDate
        //{
        //    get => endDate;
        //    set
        //    {
        //        endDate = value;
        //        OnPropertyChanged();
        //    }
        //}

 

        //public System.Windows.Input.ICommand SaveCampaignCommand { get; }

        //private async Task SaveCampaignAsync()
        //{
        //    try
        //    {
        //        var campaign = new Campaigns
        //        {
        //            Name = CampaignName,
        //            StartDate = StartDate,
        //            EndDate = EndDate, // Cambiar si tienes una fecha de finalización
        //           // State = CampaignState
        //        };

        //        //var result = await campaignService.CreateCampaignAsync(campaign);

        //        //if (result != null)
        //        //{
        //        //    // Éxito: la campaña se creó con éxito
        //        //    await Application.Current.MainPage.DisplayAlert("Éxito", "La campaña se creó con éxito.", "OK");
        //        //}
        //        //else
        //        //{
        //        //    // Error: no se pudo crear la campaña
        //        //    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo crear la campaña.", "OK");
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
        //    }
        //}


    }
}
