using AlbumsGalore.Controllers;
using AlbumsGalore.Server.DataAccess;
using AlbumsGalore.Server.Models;
using AlbumsGalore.Server.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AlbumsGalore.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ILogger<AlbumController> _logger;
        private AlbumDataAccessLayer? objAlbum = null;
        private readonly IConfiguration _configuration;
        public AlbumController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<AlbumController>();
            _logger.LogWarning("This is a WARNING message");
            _logger.LogInformation("This is an INFORMATION message");
            objAlbum = new AlbumDataAccessLayer(loggerFactory);
        }

        [HttpGet]
        [Route("Search/{searchString}")]
        public List<AlbumExtend> Search(string searchString)
        {

            List<AlbumExtend> albums = objAlbum!.Search(searchString);
            return albums;
        }

        [HttpPut]
        [Route("UpdateAlbum")]
        public int UpdateAlbum(AlbumExtend album)
        {
            try
            {
                return objAlbum!.UpdateAlbum(album);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return -1;
            }
        }

        [HttpPost]
        [Route("AddAlbum")]
        public int AddAlbum(AlbumExtend album)
        {
            try
            {
                return objAlbum!.AddAlbum(album);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        [HttpGet]
        [Route("GetAlbumsByStatusId/{statusId}")]
        public List<AlbumExtend> GetAlbumsByStatusId(int statusId)
        {
            List<AlbumExtend> list = objAlbum!.GetAlbumsByStatusId(statusId);
            return list;
        }

        [HttpGet]
        [Route("GetAlbumsByArtistId/{artistId}")]
        public IEnumerable<AlbumExtend> GetAlbumsByArtistId(int artistId)
        {
            List<AlbumExtend> list = objAlbum!.GetAlbumsByArtistId(artistId);
            return list;
        }

        /// <summary>
        /// This method does not return the objects attached to the album
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAlbumByAlbumIdBareObject/{albumId}")]
        public AlbumExtend GetAlbumByAlbumIdBareObject(int albumId)
        {
            AlbumExtend list = objAlbum!.GetAlbumByAlbumIdBareObject(albumId);
            return list;
        }

        [HttpGet]
        [Route("GetAlbumByAlbumId/{albumId}")]
        public List<AlbumExtend> GetAlbumByAlbumIdList(int albumId)
        {
            List<AlbumExtend> list = objAlbum!.GetAlbumByAlbumId(albumId);
            return list;
        }

        [HttpGet]
        [Route("GetAlbumsByUserId/{userId}")]
        public List<AlbumExtend> GetAlbumsByUserId(int userId)
        {
            List<AlbumExtend>? list = objAlbum!.GetAlbumsByUserId(userId);
            
            if (list == null)
            {
                return new List<AlbumExtend>();
            }
            else
            {
                return list;
            }
            
        }

        [HttpGet]
        [Route("GetSongsByAlbumId/{albumId}")]
        public IEnumerable<Song> GetSongsByAlbumId(int albumId)
        {
            List<Song> list = objAlbum!.GetSongsByAlbumId(albumId);
            return list;
        }

        [HttpGet]
        [Route("GetSongsByUserId/{userId}")]
        public IEnumerable<SongExtend> GetSongsByUserId(int userId)
        {
            List<SongExtend> list = objAlbum!.GetSongsByUserId(userId);
            return list;
        }

        [HttpGet]
        [Route("GetMediaList")]
        public IEnumerable<MediaType> GetMediaList()
        {
            List<MediaType> list = objAlbum!.GetMedia();
            return list;
        }

        [HttpGet]
        [Route("GetAlbumConditionTypes")]
        public IEnumerable<AlbumConditionType> GetAlbumConditionTypes()
        {
            List<AlbumConditionType> list = objAlbum!.GetAlbumConditionTypes();
            return list;
        }

        [HttpGet]
        [Route("GetAlbumStatusTypes")]
        public IEnumerable<AlbumStatus> GetAlbumStatusTypes()
        {
            List<AlbumStatus> list = objAlbum!.GetAlbumStatusTypes();
            return list;
        }

        [HttpGet]
        [Route("GetGenres")]
        public IEnumerable<Genre> GetGenres()
        {
            List<Genre> list = objAlbum!.GetGenres();
            return list;
        }
        [HttpGet]
        [Route("GetStyles")]
        public IEnumerable<Style> GetStyles()
        {
            List<Style> list = objAlbum!.GetStyles();
            return list;
        }

        [HttpGet]
        [Route("GetAlbumMetaData/{albumName}/{artistName}")]
        public AlbumExtend GetAlbumMetaData(string albumName, string artistName)
        {
            AlbumExtend album = new AlbumExtend()
            {
                AlbumId = 0,
                AlbumName = albumName,
                ArtistName = artistName
                //Description = ""
            };
            try
            {
                album = CommonFunctions.PopulateAlbumFromDiscogs(_configuration, album);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new AlbumExtend();
            }

            return album;
        }

        [HttpDelete]
        [Route("DeleteAlbumMusician/{musicianId}/{albumId}")]
        public int DeleteAlbumMusician(int musicianId, int albumId)
        {
            return objAlbum!.DeleteAlbumMusician(musicianId, albumId);
        }

        [HttpDelete]
        [Route("DeleteAlbum/{id}")]
        public int DeleteAlbum(int id)
        {
            return objAlbum!.DeleteAlbum(id);
        }

        [HttpDelete]
        [Route("DeleteSong/{id}")]
        public int DeleteSong(int id)
        {
            return objAlbum!.DeleteSong(id);
        }
    }
}
