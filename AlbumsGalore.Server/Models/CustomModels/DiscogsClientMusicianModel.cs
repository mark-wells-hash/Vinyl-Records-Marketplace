using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using Azure;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsMusicians
{
    public interface IDiscogsClientMusicianModel
    {
        Task<DiscogsMusician> OnGet(string resourceUrl);
    }
    public class DiscogsClientMusicianModel : IDiscogsClientMusicianModel
    {
        private readonly HttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public DiscogsClientMusicianModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscogsMusician> OnGet(string resourceUrl)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{resourceUrl}?key=JlfkpVDRbNEVDhqVBpoS&secret=yhatsZVudGZzExRllgDWPzdOrylLHEIR")
            {
                Headers =
            {
                { HeaderNames.Accept, "application/vnd.discogs.v2.discogs+json" },
                { HeaderNames.UserAgent, "VinylRecordMarketPlace" }
            }
            };
            try
            {
                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                
                    var result = await httpResponseMessage.Content.ReadAsStringAsync();
                    var thisResult = JsonConvert.DeserializeObject<DiscogsMusician>(result);
                    Debug.WriteLine("DiscogsSearches " + thisResult);
                    return thisResult!;
            }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return new DiscogsMusician();
        }
    }
}
