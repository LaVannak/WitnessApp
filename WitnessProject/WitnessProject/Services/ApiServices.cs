using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WitnessProject.Models;

namespace WitnessProject.Services
{
    class ApiServices
    {
        

        public async Task<bool>  RegisterWitness(Witness witness)
        {
            var json = JsonConvert.SerializeObject(witness);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8,"application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer","Key will be insert");
            var witnessURL = "https://witnesspro.azurewebsites.net/api/Witnesses";
            var response = await httpClient.PostAsync(witnessURL, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<Witness> findWitness(string email)
        {
            var httpClient = new HttpClient();
            var witnessAPIURL = "http://witnesspro.azurewebsites.net/api/Witnesses";
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "");
            //var json = await httpClient.GetStringAsync($"{witnessAPIURL}?email={email}");

            var json = await httpClient.GetStringAsync("http://witnesspro.azurewebsites.net/api/Witnesses?email=" +email);
            return JsonConvert.DeserializeObject<Witness>(json);
            
        }

        public async Task<List<Witness>> WitnessList()
        {
            var httpClient = new HttpClient();
            var witnessAPIURL = "http://witnesspro.azurewebsites.net/api/Witnesses";

            var json = await httpClient.GetStringAsync(witnessAPIURL);

            return JsonConvert.DeserializeObject<List<Witness>>(json);

        }
        public async Task<List<Incident>> GetIncidentList()
        {
            var httpClient = new HttpClient();
            var IncidentAPIURL = "https://witnesspro.azurewebsites.net/api/incidents";

            var json = await httpClient.GetStringAsync($"{IncidentAPIURL}");

            return JsonConvert.DeserializeObject<List<Incident>>(json);

        }
    }
}
