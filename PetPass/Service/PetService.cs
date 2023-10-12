using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{

    internal class PetService : IPetService
    {
        private readonly HttpClient _httpClient;



        public PetService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44313/");
        }
        public async Task<bool> CreatePet(Pet pet)
        {
            try
            {
                if (pet == null) return false;



                string petJson = JsonConvert.SerializeObject(pet);
                var content = new StringContent(petJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("PetPass/Pets/CreatePet", content);



                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
