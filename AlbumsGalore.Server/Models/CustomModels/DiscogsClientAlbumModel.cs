using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using Azure;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums
{
    public interface IDiscogsClientAlbumModel
    {
        Task<DiscogsAlbum> OnGet(string resourceUrl);
    }
    public class DiscogsClientAlbumModel : IDiscogsClientAlbumModel
    {
        private readonly HttpClient _httpClient;
        //private readonly string _remoteServiceBaseUrl;

        public DiscogsClientAlbumModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscogsAlbum> OnGet(string resourceUrl)
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
                    var thisResult = JsonConvert.DeserializeObject<DiscogsAlbum>(result);
                    Debug.WriteLine("DiscogsSearches " + thisResult);
                    return thisResult!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return new DiscogsAlbum();
        }
    }
}
