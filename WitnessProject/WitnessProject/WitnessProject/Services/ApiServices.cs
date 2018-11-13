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
            var witnessURL = "https://witnes.azurewebsites.net/api/Witnes";
            var response = await httpClient.PostAsync(witnessURL, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> NewIncident(Incident incident)
        {
            var json = JsonConvert.SerializeObject(incident);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer","Key will be insert");
            var incidentURL = "https://witnes.azurewebsites.net/api/incidents";
            var response = await httpClient.PostAsync(incidentURL, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateWitness(Witness witnes)
        {
            var json = JsonConvert.SerializeObject(witnes);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer","Key will be insert");
            var witnessAPIURL = "https://witnes.azurewebsites.net/api/Witnes";
            var response = await httpClient.PutAsync(string.Concat(witnessAPIURL,TSvr.WId), content);

            return response.IsSuccessStatusCode;
        }

        public async Task<Witness> findWitness(string email)
        {
            var httpClient = new HttpClient();
            //var witnessAPIURL = "https://witnes.azurewebsites.net/api/Witnes";
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "");
            //var json = await httpClient.GetStringAsync($"{witnessAPIURL}?email={email}");

            var json = await httpClient.GetStringAsync("https://witnes.azurewebsites.net/api/Witnes?email=" + email);
            return JsonConvert.DeserializeObject<Witness>(json);
            
        }

        public async Task<Witness> GetWitness(int id)
        {
            var httpClient = new HttpClient();
            //var witnessAPIURL = "https://witnes.azurewebsites.net/api/Witnes";
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "");
            //var json = await httpClient.GetStringAsync($"{witnessAPIURL}?email={email}");

            var json = await httpClient.GetStringAsync("https://witnes.azurewebsites.net/api/Witnes?Id=" + id);
            return JsonConvert.DeserializeObject<Witness>(json);

        }

        public async Task<List<Witness>> WitnessList()
        {
            var httpClient = new HttpClient();
            var witnessAPIURL = "https://witnes.azurewebsites.net/api/Witnes";

            var json = await httpClient.GetStringAsync(witnessAPIURL);

            return JsonConvert.DeserializeObject<List<Witness>>(json);

        }

        public async Task<List<Incident>> GetIncidentList()
        {
            var httpClient = new HttpClient();
            var IncidentAPIURL = "https://witnes.azurewebsites.net/api/incidents";

            var json = await httpClient.GetStringAsync($"{IncidentAPIURL}");

            return JsonConvert.DeserializeObject<List<Incident>>(json);

        }
    }
}
