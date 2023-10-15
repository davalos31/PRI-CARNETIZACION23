using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public interface IPatrol
    {
        Task<Person> GetPersonByIdAsync(int personId);
        Task<Zone> GetZoneByIdAsync(int zoneId);
        Task<Campaign> GetCampaignByIdAsync(int campaignId);
        Task<List<Person>> GetAllPersonsAsync();
        Task<List<Patrol1>> GetPatrolsAsync();
        Task<Patrol1> CreatePatrolAsync(Patrol1 patrol);
        Task<List<Reports>> GetPatrolsByZoneAsync();
        Task<List<Reports>> GetReportByPet();

        Task<List<Campaign>> GetCampaignsAsync();
        Task<List<Zone>> GetZonesAsync();
    }

}
