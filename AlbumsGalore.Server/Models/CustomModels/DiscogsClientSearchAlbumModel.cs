using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using Azure;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums
{
    public interface IDiscogsClientSearchAlbumModel
    {
        Task<DiscogsAlbumSearch> OnGet(string albumName, string artistName);
    }
    public class DiscogsClientSearchAlbumModel : IDiscogsClientSearchAlbumModel
    {
        private readonly HttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public DiscogsClientSearchAlbumModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscogsAlbumSearch> OnGet(string albumName, string artistName)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.discogs.com/database/search?q={artistName}&title={albumName}&type=master&key=JlfkpVDRbNEVDhqVBpoS&secret=yhatsZVudGZzExRllgDWPzdOrylLHEIR")
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
                    var thisResult = JsonConvert.DeserializeObject<DiscogsAlbumSearch>(result);
                    Debug.WriteLine("DiscogsSearches " + thisResult);
                    return thisResult!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return new DiscogsAlbumSearch();
        }
    }
}
