using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using Azure;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsArtists
{
    public interface IDiscogsClientSearchArtistModel
    {
        Task<DiscogsArtistSearch> OnGet(string artistName);
    }
    public class DiscogsClientSearchArtistModel : IDiscogsClientSearchArtistModel
    {
        private readonly HttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public DiscogsClientSearchArtistModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscogsArtistSearch> OnGet(string artistName)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.discogs.com/database/search?q={artistName}&type=artist&key=JlfkpVDRbNEVDhqVBpoS&secret=yhatsZVudGZzExRllgDWPzdOrylLHEIR")
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
                    var thisResult = JsonConvert.DeserializeObject<DiscogsArtistSearch>(result);
                    Debug.WriteLine("DiscogsSearches " + thisResult);
                    return thisResult!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return new DiscogsArtistSearch();
        }
    }
}
