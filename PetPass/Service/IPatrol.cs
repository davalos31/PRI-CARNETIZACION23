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
	
		
	

		Task<List<Campaigns>> GetCampaignsAsync(string token);
		

		Task<bool> CreatePatrolAsyncApi(string token, Patrol1 patrol);

		Task<List<Zone>> GetZonesAsyncApi(string token);

		Task<List<PatrolInfo>> GetPatrolAsyncApi(string token);

		Task<Patrol1> GetPatrolDetailsAsyncApi(string token, int patrolId);
	}
}
