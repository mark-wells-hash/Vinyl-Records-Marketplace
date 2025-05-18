using Azure.Core;
using Microsoft.EntityFrameworkCore;
using AlbumsGalore.Server.Utilities;
using System.Reflection;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using AlbumsGalore.Server.Models;

namespace AlbumsGalore.Server.DataAccess
{
    public class ArtistDataAccessLayer
    {
        ArtistsContext artists = new ArtistsContext();
        private readonly ILogger<ArtistDataAccessLayer> _logger;

        public ArtistDataAccessLayer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ArtistDataAccessLayer>();
            _logger.LogWarning("This is a WARNING message ArtistDataAccessLayer");
            _logger.LogInformation("This is an INFORMATION message");
        }
        public IEnumerable<Artist> GetAllArtists()
        {
            try
            {
                List<Artist> art = artists.Artists.ToList();
                foreach (var artist in art)
                {
                    artist.Albums.ToList();
                    artist.Musicians.ToList();
                }
                return art;
            }
            catch
            {
                throw;
            }
        }

        public Artist GetArtistArtistId(int artistId)
        {
            try
            {
                List<Artist> newList = artists.Artists.Where(m => m.ArtistId == artistId)
                .Select(m => new Artist
                {
                    Name = m.Name,
                    Description = m.Description,
                    ArtistId = m.ArtistId,
                    UserId = m.UserId,
                    ThumbnailUrl = m.ThumbnailUrl,
                    CoverImageUrl = m.CoverImageUrl,
                    Albums = m.Albums,
                    Musicians = m.Musicians,
                    DateInserted = m.DateInserted,
                    DiscogResourceUrl = m.DiscogResourceUrl,
                }).ToList();
                return newList[0];
            }
            catch
            {
                throw;
            }

        }

        public List<Artist> GetArtistsByUserId(int userId)
        {
            List<Artist> newList = artists.Artists.Where(m => m.UserId == userId)
            .Select(m => new Artist
            {
                Name = m.Name,
                Description = m.Description,
                ArtistId = m.ArtistId,
                UserId = m.UserId,
            }).ToList();

            return newList;
        }
        //To Add new Artist record     
        public int AddArtist(Artist artistObj)
        {

            try
            {
                if (artistObj.ArtistId == 0)
                {
                    artists.Artists.Add(artistObj);
                }
               
                foreach (Musician musician in artistObj.Musicians)
                {
                    if (musician.MusicianId == 0)
                    {
                        artists.Musicians.Add(musician);
                    }
                }

                artists.SaveChanges();
                return artistObj.ArtistId;
            }
            catch
            {
                throw;
            }
        }


        //To Update the records of a particluar Artist    
        public int UpdateArtist(Artist artistObj)
        {
            try
            {
                artists.Entry(artistObj).State = EntityState.Modified;

                foreach (Album album in artistObj.Albums)
                {
                    foreach (Song song in album.Songs)
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
                    if (album.AlbumId > 0)
                    {
                        artists.Entry(album).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Albums.Add(album);
                    }
                }
                foreach (Musician musician in artistObj.Musicians)
                {
                    if (musician.MusicianId > 0)
                    {
                        artists.Entry(musician).State = EntityState.Modified;
                    }
                    else
                    {
                        artists.Musicians.Add(musician);
                    }

                }

                artists.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular Artist    

        public List<Musician> GetMusiciansByArtistId(int artistId)
        {

            List<Musician> newList = artists.Musicians.Where(m => m.ArtistId == artistId)
            .Select(m => new Musician
            {
                MusicianId = m.MusicianId,
                MusicianName = m.MusicianName,
                Description = m.Description,
                ArtistId = m.ArtistId
                //MediaName = m.MediaTypeNavigation.MediaName,

            }).ToList();
            return newList;
        }

        public List<Musician> GetMusiciansByMusicianName(string musicianName)
        {

            List<Musician> newList = artists.Musicians.Where(m => m.MusicianName!.StartsWith(musicianName))
            .Select(m => new Musician
            {
                MusicianId = m.MusicianId,
                MusicianName = m.MusicianName,
                Description = m.Description,
                ArtistId = m.ArtistId
                //MediaName = m.MediaTypeNavigation.MediaName,

            }).ToList();
            return newList;
        }

        public List<Musician> GetMusiciansByUserId(int userId)
        {

            List<Musician> newList = artists.Musicians.Where(m => m.UserId == userId)
            .Select(m => new Musician
            {
                MusicianId = m.MusicianId,
                MusicianName = m.MusicianName,
                Description = m.Description,
                ArtistId = m.ArtistId
                //MediaName = m.MediaTypeNavigation.MediaName,

            }).ToList();
            return newList;
        }



        public int DeleteArtist(int ArtistId)
        {
            try
            {
                //Musician musician = artists.Musicians.Find(id);
                Artist artist = artists.Artists.Find(ArtistId)!;
                List<Musician> musicians = GetMusiciansByArtistId(ArtistId);
                foreach (Musician musician in musicians)
                {
                    artists.Musicians.Remove(musician);

                }
                List<Album> albums = GetAlbumsForArtistDelete(ArtistId);
                foreach (Album album in albums)
                {
                    foreach (Song song in album.Songs)
                    {
                        artists.Songs.Remove(song);
                    }
                    artists.Albums.Remove(album);
                }
                artists.Artists.Remove(artist);
                artists.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteMusician(int musicianID)
        {
            try
            {
                Musician musician = artists.Musicians.Find(musicianID)!;
                artists.Musicians.Remove(musician);
                artists.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public List<Album> GetAlbumsForArtistDelete(int albumId)
        {
            try
            {
                List<Album> newList = artists.Albums.Where(m => m.AlbumId == albumId)
           .Select(m => new Album
           {
                AlbumName = m.AlbumName,
                AlbumId = m.AlbumId,
                Description = m.Description,
                ArtistId = m.ArtistId,
                Songs = m.Songs

            }).ToList();
                
                return newList;
            }
            catch
            {
                throw;
            }
        }
    }
}
