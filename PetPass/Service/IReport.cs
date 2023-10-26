using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public interface IReport
    {
      

        Task<List<BrigadierData>> GetBrigadierDataAsync(string token);
        Task<List<BrigadierData>> GetZoneAndTotalDogs(string token);

        Task<List<BrigadierData>> GetZoneReport(string token);
    }
}
