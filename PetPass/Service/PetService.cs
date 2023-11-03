﻿using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
	internal class PetService : BaseService, IPetService
	{

		public PetService() : base()
		{

		}

		public async Task<bool> CreatePet(PetCreated pet)

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