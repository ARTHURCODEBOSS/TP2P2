using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;

namespace TP2P2.Service
{
    public class WSService
    {
        private HttpClient client;

        public HttpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        public WSService()
        {
            Client = new HttpClient();
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://convertisseurlopes-effgfwfuh2hfbdde.francecentral-01.azurewebsites.net/api/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Serie>> GetSeriesAsync(string nomControleur)
        {
            try
            {
                return await Client.GetFromJsonAsync<List<Serie>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Serie> PostSeriesAsync(Serie serie)
        {
            try
            {
                return await Client.PostAsJsonAsync<Serie>("series", serie).Result.Content.ReadFromJsonAsync<Serie>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PutSeriesAsync(Serie serie)
        {
            try
            {
                var response = await Client.PutAsJsonAsync($"series/{serie.Serieid}", serie);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSeriesAsync(int id)
        {
            try
            {
                var response = await Client.DeleteAsync($"series/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        

    }
}
