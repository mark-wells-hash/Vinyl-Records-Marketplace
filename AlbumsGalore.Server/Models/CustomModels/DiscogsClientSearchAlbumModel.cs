using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums
{
    public interface IDiscogsClientSearchAlbumModel
    {
        Task<DiscogsAlbumSearch> OnGet(string albumName, string artistName);
    }
    public class DiscogsClientSearchAlbumModel : IDiscogsClientSearchAlbumModel
    {
        private readonly HttpClient _httpClient;
        private string _resourceUrl;
        private string _discogsKey;
        private string _discogsSecret;

        public DiscogsClientSearchAlbumModel(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _resourceUrl = configuration!["Discogs:SearchURL"]!;
            _discogsKey = configuration!["Discogs:Key"]!;
            _discogsSecret = configuration!["Discogs:Secret"]!;
        }

        public async Task<DiscogsAlbumSearch> OnGet(string albumName, string artistName)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_resourceUrl}?q={artistName}&title={albumName}&type=master&key=" + _discogsKey + "&secret=" + _discogsSecret)
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
