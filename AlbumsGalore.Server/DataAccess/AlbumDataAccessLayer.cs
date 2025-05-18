using Microsoft.EntityFrameworkCore;
using AlbumsGalore.Server.Utilities;
using System.Diagnostics;
using AlbumsGalore.Server.Models;

namespace AlbumsGalore.Server.DataAccess
{
    public class AlbumDataAccessLayer
    {
        ArtistsContext artists = new ArtistsContext();
        private readonly ILogger<AlbumDataAccessLayer> _logger;
        public AlbumDataAccessLayer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AlbumDataAccessLayer>();
            _logger.LogWarning("This is a WARNING message ArtistDataAccessLayer");
            _logger.LogInformation("This is an INFORMATION message");
        }

        public AlbumDataAccessLayer() { }

        public List<AlbumGenre> GetAlbumGenre(int albumId)
        {
            List<AlbumGenre> existingGenre = artists.AlbumGenres.Where(m => m.AlbumId == albumId).ToList();
            return existingGenre;
        }

        public int AddAlbum(AlbumExtend albumObj)
        {
            try
            {
                int albumId = albumObj.AlbumId;
                if (albumObj.AlbumId == 0)
                {
                    Album album = new Album()
                    {
                        ArtistId = albumObj.ArtistId,
                        AlbumName = albumObj.AlbumName,
                        Description = albumObj.Description,
                        UserId = albumObj.UserId,
                        AlbumConditionTypeId = albumObj.AlbumConditionTypeId,
                        ReleaseDate = albumObj.ReleaseDate,
                        DiscogResourceUrl = albumObj.DiscogResourceUrl,
                        ThumbnailUrl = albumObj.ThumbnailUrl,
                        CoverImageUrl = albumObj.CoverImageUrl,
                        Label = albumObj.Label,
                        AlbumStatusId = albumObj.AlbumStatusId,
                        Price = albumObj.Price,
                        Songs = albumObj.Songs
                    };
                    artists.Albums.Add(album);
                    artists.SaveChanges();
                    albumId = album.AlbumId;
                }

                /**************** Items waiting for AlbumId **************************/
                Debug.WriteLine(albumId + " : " + albumObj.AlbumId);

                foreach (Song song in albumObj.Songs)
                {
                    song.AlbumId = albumId;
                    if (song.SongId > 0)
                    {
                        artists.Entry(song).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Songs.Add(song);
                    }
                }

                List<string> musicianNames = new List<string>();
                foreach (Musician musician in albumObj.Musicians)
                {
                    musician.ArtistId = albumObj.ArtistId;
                    musicianNames.Add(musician.MusicianName!);
                    if (musician.MusicianId > 0)
                    {
                        artists.Entry(musician).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Musicians.Add(musician);
                    }

                }
                foreach (AlbumGenre genre in albumObj.AlbumGenres)
                {
                    AlbumGenre newGenre = new AlbumGenre()
                    {
                        AlbumId = albumId,
                        GenreId = genre.GenreId
                    };
                    artists.AlbumGenres.Add(newGenre);
                }

                foreach (AlbumStyle style in albumObj.AlbumStyles)
                {
                    AlbumStyle newStyle = new AlbumStyle()
                    {
                        AlbumId = albumId,
                        StyleId = style.StyleId
                    };
                    artists.AlbumStyles.Add(newStyle);
                }

                foreach (AlbumMediaType mediaType in albumObj.AlbumMediaTypes)
                {
                    AlbumMediaType newMediaType = new AlbumMediaType()
                    {
                        AlbumId = albumId,
                        MediaTypeId = mediaType.MediaTypeId
                    };
                    artists.AlbumMediaTypes.Add(newMediaType);
                }

                artists.SaveChanges();

                foreach (string x in musicianNames)
                {
                    var musician = artists.Musicians.Where(m => m.MusicianName == x).FirstOrDefault();
                    AlbumMusician newMusician = new AlbumMusician()
                    {
                        AlbumId = albumId,
                        MusicianId = musician!.MusicianId
                    };
                    artists.AlbumMusicians.Add(newMusician);
                }

                artists.SaveChanges();


                return albumId;
            }
            catch
            {
                throw;
            }

        }
        public int UpdateAlbum(AlbumExtend albumObj)
        {
            try
            {
                //Removing this call from the update doing on insert only
                //albumObj = CommonFunctions.PopulateAlbumFromDiscogs(albumObj);
                List<AlbumGenre> existingGenre = artists.AlbumGenres.Where(m => m.AlbumId == albumObj.AlbumId).ToList();
                List<AlbumStyle> existingStyle = artists.AlbumStyles.Where(m => m.AlbumId == albumObj.AlbumId).ToList();
                List<AlbumMediaType> existingMediaType = artists.AlbumMediaTypes.Where(m => m.AlbumId == albumObj.AlbumId).ToList();
                List<AlbumMusician> existingAlbumMusicians = artists.AlbumMusicians.Where(m => m.AlbumId == albumObj.AlbumId).ToList();

                var newGenreList = CommonFunctions.ListDifference((List<AlbumGenre>)albumObj.AlbumGenres, existingGenre, "get_AlbumId", "get_GenreId");
                var deleteGenreList = CommonFunctions.ListDifference(existingGenre, (List<AlbumGenre>)albumObj.AlbumGenres, "get_AlbumId", "get_GenreId");

                foreach (AlbumGenre genre in newGenreList)
                {
                    AlbumGenre newGenre = new AlbumGenre()
                    {
                        AlbumId = genre.AlbumId,
                        GenreId = genre.GenreId
                    };
                    artists.AlbumGenres.Add(newGenre);
                }

                foreach (AlbumGenre genre in deleteGenreList)
                {
                    artists.AlbumGenres.Remove(genre);
                }

                var newStyleList = CommonFunctions.ListDifference((List<AlbumStyle>)albumObj.AlbumStyles, existingStyle, "get_AlbumId", "get_StyleId");
                var deleteStyleList = CommonFunctions.ListDifference(existingStyle, (List<AlbumStyle>)albumObj.AlbumStyles, "get_AlbumId", "get_StyleId");

                foreach (AlbumStyle style in newStyleList)
                {
                    AlbumStyle newStyle = new AlbumStyle()
                    {
                        AlbumId = style.AlbumId,
                        StyleId = style.StyleId
                    };
                    artists.AlbumStyles.Add(newStyle);
                }

                foreach (AlbumStyle style in deleteStyleList)
                {
                    artists.AlbumStyles.Remove(style);
                }

                var newMediaTypeList = CommonFunctions.ListDifference((List<AlbumMediaType>)albumObj.AlbumMediaTypes, existingMediaType, "get_AlbumId", "get_MediaTypeId");
                var deleteMediaTypeList = CommonFunctions.ListDifference(existingMediaType, (List<AlbumMediaType>)albumObj.AlbumMediaTypes, "get_AlbumId", "get_MediaTypeId");

                foreach (AlbumMediaType mediaType in newMediaTypeList)
                {
                    AlbumMediaType newMediaType = new AlbumMediaType()
                    {
                        AlbumId = mediaType.AlbumId,
                        MediaTypeId = mediaType.MediaTypeId
                    };
                    artists.AlbumMediaTypes.Add(newMediaType);
                }

                foreach (AlbumMediaType mediaType in deleteMediaTypeList)
                {
                    artists.AlbumMediaTypes.Remove(mediaType);
                }

                foreach (Song song in albumObj.Songs)
                {
                    if (song.SongId > 0)
                    {
                        artists.Entry(song).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Songs.Add(song);
                    }

                }

                List<string> musicianNames = new List<string>();
                foreach (Musician musician in albumObj.Musicians)
                {
                    if (musician.MusicianId > 0)
                    {
                        AlbumMusician newMusician = new AlbumMusician()
                        {
                            AlbumId = albumObj.AlbumId,
                            MusicianId = musician!.MusicianId
                        };

                        if (!existingAlbumMusicians.Contains(newMusician, new GenericCompare<AlbumMusician>(m => m.MusicianId)))
                        {
                            artists.AlbumMusicians.Add(newMusician);
                        }
                        artists.Entry(musician).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Musicians.Add(musician);
                        musicianNames.Add(musician.MusicianName!);
                    }

                }
                artists.Entry(albumObj).State = EntityState.Modified;
                artists.SaveChanges();

                foreach (string x in musicianNames)
                {
                    var musician = artists.Musicians.Where(m => m.MusicianName == x).FirstOrDefault();
                    AlbumMusician newMusician = new AlbumMusician()
                    {
                        AlbumId = albumObj.AlbumId,
                        MusicianId = musician!.MusicianId
                    };
                    artists.AlbumMusicians.Add(newMusician);
                }

                artists.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public List<AlbumExtend> Search(string searchString)
        {
            try
            {
                var listAlbum =
                          (from Albums in artists.Albums
                           join Artists in artists.Artists
                             on Albums.ArtistId equals Artists.ArtistId
                           where (Albums.AlbumName!.StartsWith(searchString) || Artists.Name!.StartsWith(searchString)) && Albums.AlbumStatusId == 1
                           select Albums).OrderByDescending(x => x.DateInserted);
                //TODO: Refactor the query to create the objects in the select (see SalesDataAccess function GetShoppingCartByUser)
                //instead of using the populateAlbum function
                List<AlbumExtend> newList = populateAlbum(listAlbum);
               
                return newList;
                //return new List<AlbumExtend>();
            }
            catch
            {
                throw;
                //Console.WriteLine(ex.Message);
                //return null;
            }

        }

        public List<AlbumExtend> GetAlbumsByStatusId(int StatusId)
        {
            IQueryable<Album> listAlbum = artists.Albums.Where(m => m.AlbumStatusId == StatusId).OrderByDescending(x => x.DateInserted);
            List<AlbumExtend> newList = populateAlbum(listAlbum);
            return newList;
        }
        public List<AlbumExtend> GetAlbumsByArtistId(int ArtistId)
        {
            IQueryable<Album> listAlbum = artists.Albums.Where(m => m.ArtistId == ArtistId);
            List<AlbumExtend> newList = populateAlbum(listAlbum);
            return newList;
        }

        public AlbumExtend GetAlbumByAlbumIdBareObject(int albumId)
        {
            try
            {
                ArtistsContext artistsTemp = new ArtistsContext();
                Album album = artists.Albums.Find(albumId)!;
                AlbumExtend albumExtend = new AlbumExtend();
                //{
                albumExtend.AlbumName = album.AlbumName;
                albumExtend.AlbumId = album.AlbumId;
                albumExtend.Description = album.Description;
                albumExtend.ArtistId = album.ArtistId;
                albumExtend.Songs = album.Songs;
                albumExtend.AlbumConditionTypeId = album.AlbumConditionTypeId!;
                albumExtend.AlbumConditionType = album.AlbumConditionType!;
                albumExtend.ConditionName = album.AlbumConditionType != null ? album.AlbumConditionType.ConditionName : string.Empty;
                albumExtend.ConditionDescription = album.AlbumConditionType != null ? album.AlbumConditionType.ConditionDescription : string.Empty;
                albumExtend.AlbumStatus = album.AlbumStatus!;
                albumExtend.AlbumStatusId = album.AlbumStatusId!;
                albumExtend.ThumbnailUrl = album.ThumbnailUrl!;
                albumExtend.CoverImageUrl = album.CoverImageUrl!;
                albumExtend.Label = album.Label!;
                albumExtend.ArtistName = album.Artist != null ? album.Artist.Name : string.Empty;
                albumExtend.UserId = album.UserId;
                albumExtend.Price = album.Price!;
                albumExtend.AlbumMediaTypes = [.. album.AlbumMediaTypes!];
                albumExtend.AlbumMusicians = [.. album.AlbumMusicians!];
                albumExtend.AlbumGenres = [.. album.AlbumGenres!];
                albumExtend.Genres = GetGenres(album.AlbumId, artistsTemp);
                albumExtend.AlbumStyles = [.. album.AlbumStyles!];
                albumExtend.Styles = GetStyles(album.AlbumId, artistsTemp);

                return albumExtend;
            }
            catch
            {
                throw;
            }

        }

        public List<AlbumExtend> GetAlbumByAlbumId(int albumId)
        {
            IQueryable<Album> listAlbum = artists.Albums.Where(m => m.AlbumId == albumId);
            List<AlbumExtend> newList = populateAlbum(listAlbum);
            return newList;
        }

        public List<AlbumExtend>? GetAlbumsByUserId(int userId)
        {
            try
            {
                IQueryable<Album> listAlbum = artists.Albums.Where(m => m.UserId == userId).OrderByDescending(x => x.DateInserted);
                List<AlbumExtend> newList = populateAlbum(listAlbum);
                return newList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAlbumsbyUserID failed");
                return null;
            }
        }
        //TODO: Refactor references to this function to use joins to create the objects in the select (see SalesDataAccess function GetShoppingCartByUser)
        //instead of using the populateAlbum function
        public List<AlbumExtend> populateAlbum(IQueryable<Album> listAlbum)
        {
            try
            {
                ArtistsContext artistsTemp = new ArtistsContext();
                ArtistsContext artistsTemp2 = new ArtistsContext();
                List<AlbumExtend> albumList = new List<AlbumExtend>();
                foreach (var album in listAlbum)
                {
                    AlbumExtend newAlbum = new AlbumExtend();
                    newAlbum.AlbumName = album.AlbumName;
                    newAlbum.AlbumId = album.AlbumId;
                    newAlbum.Description = album.Description;
                    newAlbum.ArtistId = album.ArtistId;
                    newAlbum.ArtistName = GetAlbumArtistName(album.ArtistId, artistsTemp2);
                    newAlbum.Songs = GetSongs(album.AlbumId, artistsTemp2);
                    newAlbum.AlbumConditionTypeId = album.AlbumConditionTypeId!;
                    newAlbum.AlbumConditionType = GetAlbumCondition(album.AlbumConditionTypeId, artistsTemp);
                    newAlbum.ConditionName = newAlbum.AlbumConditionType != null ? newAlbum.AlbumConditionType.ConditionName : string.Empty;
                    newAlbum.ConditionDescription = newAlbum.AlbumConditionType != null ? newAlbum.AlbumConditionType.ConditionDescription : string.Empty;
                    newAlbum.AlbumStatusId = album.AlbumStatusId!;
                    newAlbum.AlbumStatus = GetAlbumStatus(album.AlbumStatusId, artistsTemp);
                    newAlbum.StatusName = newAlbum.AlbumStatus != null ? newAlbum.AlbumStatus.StatusName : string.Empty;
                    newAlbum.ThumbnailUrl = album.ThumbnailUrl!;
                    newAlbum.CoverImageUrl = album.CoverImageUrl!;
                    newAlbum.Label = album.Label!;
                    newAlbum.ReleaseDate = album.ReleaseDate!;
                    newAlbum.UserId = album.UserId;
                    newAlbum.Price = album.Price!;
                    newAlbum.AlbumMediaTypes = GetAlbumMediaTypes(album.AlbumId, artistsTemp2);
                    newAlbum.MediaTypes = GetMediaTypes(album.AlbumId, artistsTemp);
                    newAlbum.AlbumGenres = GetAlbumGenres(album.AlbumId, artistsTemp2);
                    newAlbum.Genres = GetGenres(album.AlbumId, artistsTemp);
                    newAlbum.AlbumMusicians = album.AlbumMusicians;
                    newAlbum.Musicians = GetMusicians(album.AlbumId, artistsTemp);
                    newAlbum.AlbumStyles = GetAlbumStyles(album.AlbumId, artistsTemp2);
                    newAlbum.Styles = GetStyles(album.AlbumId, artistsTemp);
                    albumList.Add(newAlbum);
                }
                return albumList;

                /************** Replaced the below method with the above as it is too slow ********/
                //ArtistsContext artistsTemp = new ArtistsContext();
                //List<AlbumExtend> newList = listAlbum.Select(m => new AlbumExtend()
                //{
                //    AlbumName = m.AlbumName,
                //    AlbumId = m.AlbumId,
                //    Description = m.Description,
                //    ArtistId = m.ArtistId,
                //    //Artist = m.Artist,
                //    Songs = m.Songs,
                //    AlbumConditionTypeId = m.AlbumConditionTypeId!,
                //    AlbumConditionType = m.AlbumConditionType!,
                //    StatusName = m.AlbumStatus != null ? m.AlbumStatus.StatusName : string.Empty,
                //    ConditionName = m.AlbumConditionType != null ? m.AlbumConditionType.ConditionName : string.Empty,
                //    ConditionDescription = m.AlbumConditionType != null ? m.AlbumConditionType.ConditionDescription : string.Empty,
                //    AlbumStatus = m.AlbumStatus!,
                //    AlbumStatusId = m.AlbumStatusId!,
                //    ThumbnailUrl = m.ThumbnailUrl!,
                //    CoverImageUrl = m.CoverImageUrl!,
                //    Label = m.Label!,
                //    ReleaseDate = m.ReleaseDate!,
                //    ArtistName = m.Artist != null ? m.Artist.Name : string.Empty,
                //    UserId = m.UserId,
                //    Price = m.Price!,
                //    AlbumMediaTypes = m.AlbumMediaTypes!,
                //    //MediaTypes = GetMediaTypes(m.AlbumMediaTypes!.ToList(), artistsTemp),
                //    AlbumGenres = m.AlbumGenres!,
                //    //Genres = GetGenres(m.AlbumGenres!.ToList(), artistsTemp),
                //    AlbumMusicians = m.AlbumMusicians,
                //    //Musicians = GetMusicians(m.AlbumMusicians!.ToList(), artistsTemp),
                //    AlbumStyles = m.AlbumStyles,
                //    //Styles = GetStyles(m.AlbumStyles!.ToList(), artistsTemp),
                //}
                //).ToList();
                //return newList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "populateAlbum failed");
                return new List<AlbumExtend>();
            }
        }

        private static AlbumConditionType GetAlbumCondition(int? albumConditionTypeId, ArtistsContext artistsTemp)
        {
            AlbumConditionType condition = artistsTemp.AlbumConditionTypes.Find(albumConditionTypeId)!; ;
            return condition;
        }

        private static AlbumStatus GetAlbumStatus(int? albumStatusId, ArtistsContext artistsTemp)
        {
            AlbumStatus status = artistsTemp.AlbumStatuses.Find(albumStatusId)!; ;
            return status;
        }

        private static string GetAlbumArtistName(int? artistId, ArtistsContext artistsTemp)
        {
            Artist artist = artistsTemp.Artists.Find(artistId)!; ;
            return artist.Name!;
        }

        private static List<AlbumGenre> GetAlbumGenres(int albumId, ArtistsContext artistsTemp)
        {
            var q = artistsTemp.AlbumGenres.Where(m => m.AlbumId == albumId).ToList();
            List<AlbumGenre> genreList = q;
            return genreList;
        }

        private static List<Genre> GetGenres(int albumId, ArtistsContext artistsTemp)
        {
            var q = from Genre in artistsTemp.Genres
                    where artistsTemp.AlbumGenres.Where(m => m.AlbumId == albumId).Select(o => o.GenreId).Contains(Genre.GenreId)
                    select Genre;
            List<Genre> genreList = q.ToList();
            return genreList;
        }

        private static List<AlbumMediaType> GetAlbumMediaTypes(int albumId, ArtistsContext artistsTemp)
        {
            var q = artistsTemp.AlbumMediaTypes.Where(m => m.AlbumId == albumId).ToList();
            List<AlbumMediaType> mediaList = q.ToList();
            return mediaList;
        }

        private static List<MediaType> GetMediaTypes(int albumId, ArtistsContext artistsTemp)
        {
            var q = from MediaType in artistsTemp.MediaTypes
                    where artistsTemp.AlbumMediaTypes.Where(m => m.AlbumId == albumId).Select(o => o.MediaTypeId).Contains(MediaType.MediaTypeId)
                    select MediaType;
            List<MediaType> mediaTypeList = q.ToList();
           
            return mediaTypeList;
        }

        private static List<Musician> GetMusicians(int albumId, ArtistsContext artistsTemp)
        {
            var q = from Musician in artistsTemp.Musicians
                    where artistsTemp.AlbumMusicians.Where(m => m.AlbumId == albumId).Select(o => o.MusicianId).Contains(Musician.MusicianId)
                    select Musician;
            List<Musician> musicianList = q.ToList();
            return musicianList;
        }

        private static List<AlbumStyle> GetAlbumStyles(int albumId, ArtistsContext artistsTemp)
        {
            var q = artistsTemp.AlbumStyles.Where(m => m.AlbumId == albumId).ToList();
            List<AlbumStyle> styleList = q.ToList();
            return styleList;
        }
        private static List<Style> GetStyles(int albumId, ArtistsContext artistsTemp)
        {
            var q = from Style in artistsTemp.Styles
                    where artistsTemp.AlbumStyles.Where(m => m.AlbumId == albumId).Select(o => o.StyleId).Contains(Style.StyleId)
                    select Style;
            List<Style> stylesList = q.ToList();
            return stylesList;
        }

        private static List<Song> GetSongs(int albumId, ArtistsContext artistsTemp)
        {
            var q = artistsTemp.Songs.Where(m => m.AlbumId == albumId).ToList();
            List<Song> songList = q.ToList();
            return songList;
        }

        public List<Song> GetSongsByAlbumId(int albumId)
        {

            List<Song> newList = artists.Songs.Where(m => m.AlbumId == albumId)
            .Select(m => new Song
            {
                SongName = m.SongName,
                SongId = m.SongId,
                Description = m.Description,
                AlbumId = m.AlbumId,

            }).ToList();
            return newList;
        }

        public List<SongExtend> GetSongsByUserId(int userId)
        {
            List<SongExtend> newList = artists.Songs.Where(m => m.UserId == userId)
            .Select(m => new SongExtend
            {
                SongName = m.SongName,
                SongId = m.SongId,
                Description = m.Description,
                AlbumId = m.AlbumId,
                AlbumName = m.Album!.AlbumName,
                ArtistName = m.Album.Artist!.Name,
                ConditionName = m.Album.AlbumConditionType!.ConditionName,
                ConditionDescription = m.Album.AlbumConditionType.ConditionDescription
            }).ToList();
            return newList;
        }

        public List<MediaType> GetMedia()
        {
            List<MediaType> mediaList = artists.MediaTypes.ToList();
            return mediaList;
        }

        public List<AlbumConditionType> GetAlbumConditionTypes()
        {
            List<AlbumConditionType> conditionList = artists.AlbumConditionTypes.ToList();
            return conditionList;
        }

        public List<AlbumStatus> GetAlbumStatusTypes()
        {
            List<AlbumStatus> statusList = artists.AlbumStatuses.ToList();
            return statusList;
        }
        public List<Genre> GetGenres()
        {
            List<Genre> genreList = artists.Genres.ToList();
            return genreList;
        }
        public List<Style> GetStyles()
        {
            List<Style> styleList = artists.Styles.ToList();
            return styleList;
        }

        public int DeleteAlbumMusician(int musicianID, int albumId)
        {

            try
            {
                List<AlbumMusician> albumMusician = artists.AlbumMusicians.Where(m => m.MusicianId == musicianID && m.AlbumId == albumId)
                     .Select(m => new AlbumMusician
                     {
                         AlbumMusiciansId = m.AlbumMusiciansId,
                         AlbumId = m.AlbumId,
                         MusicianId = m.MusicianId,

                     }).ToList();
                if (albumMusician.Count > 0)
                {
                    artists.AlbumMusicians.Remove(albumMusician[0]);
                    artists.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteAlbum(int albumID)
        {
            try
            {
                //IQueryable<Album> album = artists.Albums.Where(m => m.AlbumId == albumID); // GetAlbums(albumID);
                Album album = artists.Albums.Find(albumID)!;
                List<Song> songs = GetSongsByAlbumId(albumID);
                foreach (Song song in songs)
                {
                    artists.Songs.Remove(song);

                }
                artists.Albums.Remove(album);
                artists.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteSong(int songID)
        {
            try
            {
                Song song = artists.Songs.Find(songID)!;
                artists.Songs.Remove(song);
                artists.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

    }
}
