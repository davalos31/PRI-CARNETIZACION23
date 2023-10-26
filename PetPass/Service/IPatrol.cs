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
		Task<Campaigns> GetCampaignByIdAsync(int campaignId);
		Task<List<Person>> GetAllPersonsAsync();
		Task<List<Patrol1>> GetPatrolsAsync();
		Task<Patrol1> CreatePatrolAsync(Patrol1 patrol);
		Task<List<Reports>> GetPatrolsByZoneAsync();
		Task<List<Reports>> GetReportByPet();

		Task<List<Campaigns>> GetCampaignsAsync();
		//Task<List<Zone>> GetZonesAsync();

		Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol);

		Task<List<Zone>> GetZonesAsyncApi(string token);

		Task<List<Patrol1>> GetPatrolAsyncApi(string token);

		Task<Patrol1> GetPatrolDetailsAsyncApi(string token, int patrolId);
	}
}
