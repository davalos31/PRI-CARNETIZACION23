using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
   public interface ICampaign
    {
        Task<Campaigns> CreateCampaignAsync(Campaigns campaign);

        Task<Campaigns> GetCampaignAsync(int id);



        Task<List<Campaigns>> GetCampaignsAsync();
    }
}
