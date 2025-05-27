using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums
{
    public interface IDiscogsClientAlbumModel
    {
        Task<DiscogsAlbum> OnGet(string resourceUrl);
    }
    public class DiscogsClientAlbumModel : IDiscogsClientAlbumModel
    {
        private readonly HttpClient _httpClient;
        private string _discogsKey;
        private string _discogsSecret;

        public DiscogsClientAlbumModel(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _discogsKey = configuration!["Discogs:Key"]!;
            _discogsSecret = configuration!["Discogs:Secret"]!;
        }
        //JlfkpVDRbNEVDhqVBpoS&secret=yhatsZVudGZzExRllgDWPzdOrylLHEIR
        public async Task<DiscogsAlbum> OnGet(string resourceUrl)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{resourceUrl}?key=" + _discogsKey + "&secret=" + _discogsSecret)
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
