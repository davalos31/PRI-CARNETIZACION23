using Newtonsoft.Json;
using PetPass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPass.Service
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44313/"); // Reemplaza con la URL correcta
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            try
            {
                if (person == null)
                {
                    Console.WriteLine("El objeto 'person' es nulo.");
                    return null;
                }

                string personJson = JsonConvert.SerializeObject(person);
                var content = new StringContent(personJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("PetPass/People/CreateBrigadier", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(responseBody))
                    {
                        Console.WriteLine("La respuesta del servidor está vacía.");
                        return null;
                    }

                    Person createdPerson = JsonConvert.DeserializeObject<Person>(responseBody);
                    return createdPerson;
                }
                else
                {
                    Console.WriteLine($"Error al crear la persona. Código de estado HTTP: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la persona: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("PetPass/People"); // Ruta correcta de la API GET

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(content))
                    {
                        Console.WriteLine("La respuesta del servidor está vacía.");
                        return null;
                    }

                    var people = JsonConvert.DeserializeObject<List<Person>>(content);
                    return people;
                }
                else
                {
                    Console.WriteLine($"Error al obtener la lista de personas. Código de estado HTTP: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de personas: {ex.Message}");
                return null;
            }
        }
    }
}
