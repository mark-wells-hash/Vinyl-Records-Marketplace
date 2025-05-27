using Microsoft.AspNetCore.Mvc;
using AlbumsGalore.Server.Utilities;
using AlbumsGalore.Server.Models;
using AlbumsGalore.Server.DataAccess;

namespace AlbumsGalore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ArtistsController> _logger;
        private ArtistDataAccessLayer? objArtist = null;
        public ArtistsController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<ArtistsController>();
            _logger.LogWarning("This is a WARNING message");
            _logger.LogInformation("This is an INFORMATION message");
            objArtist = new ArtistDataAccessLayer(loggerFactory);
        }

        [HttpGet]
        [Route("ArtistIndex")]
        public IEnumerable<Artist> ArtistIndex()
        {
            var allArtists = objArtist!.GetAllArtists();
            return allArtists; 
        }
        [HttpPost]
        [Route("AddArtist")]
        public int AddArtist(Artist artist)
        {
            //TODO: Check to see if artist already exists. Do the same for the other entity creations-
            return objArtist!.AddArtist(artist);
        }

        [HttpGet]
        [Route("GetArtistMetaData/{artistName}")]
        public Artist GetArtistMetaData(string artistName)
        {
            Artist artist = new Artist()
            {
                ArtistId = 0,
                Name = artistName,
                Description = ""
            };
            try
            {
                artist = CommonFunctions.PopulateArtistFromDiscogs(_configuration, artist);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Artist();
            }
            
            return artist;
        }

        [HttpGet]
        [Route("ArtistDetails/{artistId}")]
        public Artist ArtistDetails(int artistId)
        {
            
            var artist = objArtist!.GetArtistArtistId(artistId);
            return artist;
        }

        [HttpGet]
        [Route("GetArtistsByUserId/{userId}")]
        public List<Artist> GetArtistsByUserId(int userId)
        {

            var artistList = objArtist!.GetArtistsByUserId(userId);
            return artistList;
        }

        [HttpPut]
        [Route("Edit")]
        public int Edit(Artist artist)
        {
            return objArtist!.UpdateArtist(artist);
        }

        [HttpGet]
        [Route("GetMusiciansByArtistId/{artistId}")]
        public IEnumerable<Musician> GetMusiciansByArtistId(int artistId)
        {
            List<Musician> list = objArtist!.GetMusiciansByArtistId(artistId);
            return list;
        }

        [HttpGet]
        [Route("GetMusiciansByMusicianName/{musicianName}")]
        public List<Musician> GetMusiciansByMusicianName(string musicianName)
        {
            List<Musician> list = objArtist!.GetMusiciansByMusicianName(musicianName);
            return list;

        }

        [HttpGet]
        [Route("GetMusiciansByUserId/{userId}")]
        public IEnumerable<Musician> GetMusiciansByUserId(int userId)
        {
            List<Musician> list = objArtist!.GetMusiciansByUserId(userId);
            return list;
        }

        [HttpDelete]
        [Route("DeleteArtist/{id}")]
        public int DeleteArtist(int id)
        {
            return objArtist!.DeleteArtist(id);
        }

        [HttpDelete]
        [Route("DeleteMusician/{id}")]
        public int DeleteMusician(int id)
        {
            return objArtist!.DeleteMusician(id);
        }
    }
}
