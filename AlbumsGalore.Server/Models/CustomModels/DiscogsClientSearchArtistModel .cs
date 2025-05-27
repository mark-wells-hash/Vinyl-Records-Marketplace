using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace AlbumsGalore.Server.Models.CustomModels.DiscogsArtists
{
    public interface IDiscogsClientSearchArtistModel
    {
        Task<DiscogsArtistSearch> OnGet(string artistName);
    }
    public class DiscogsClientSearchArtistModel : IDiscogsClientSearchArtistModel
    {
        private readonly HttpClient _httpClient;
        private string _resourceUrl;
        private string _discogsKey;
        private string _discogsSecret;

        public DiscogsClientSearchArtistModel(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _resourceUrl = configuration!["Discogs:SearchURL"]!;
            _discogsKey = configuration!["Discogs:Key"]!;
            _discogsSecret = configuration!["Discogs:Secret"]!;
        }

        public async Task<DiscogsArtistSearch> OnGet(string artistName)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_resourceUrl}?q={artistName}&type=artist&key=" + _discogsKey + "&secret=" + _discogsSecret)
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
