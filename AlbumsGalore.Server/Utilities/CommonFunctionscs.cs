using AlbumsGalore.Server.Models;
using AlbumsGalore.Server.Models.CustomModels.DiscogsArtists;
using AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums;
using AlbumsGalore.Server.Models.CustomModels.DiscogsMusicians;

using System.Reflection;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;

namespace AlbumsGalore.Server.Utilities
{
    public static class CommonFunctions
    {
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public static AlbumExtend PopulateAlbumFromDiscogs(AlbumExtend album)
        {
            var httpClient = new HttpClient();
            DiscogsClientSearchAlbumModel discogsClientSearchModel = new DiscogsClientSearchAlbumModel(httpClient);
            DiscogsClientAlbumModel discogsClientAlbumModel = new DiscogsClientAlbumModel(httpClient);
            var fromDiscogs = new DiscogsAlbumSearch();
            var fromDiscogsAlbum = new DiscogsAlbum();
            try
            {
                Task.Run(async () =>
            {
                fromDiscogs = await discogsClientSearchModel.OnGet(album.AlbumName!, album.ArtistName!);
                Console.WriteLine("Asynchronous work completed.");
            }).Wait();
                var firstResult = fromDiscogs.results.FirstOrDefault();
                if (firstResult != null)
                {
                    album.Label = album.Label.IsNullOrEmpty() ? firstResult.label.FirstOrDefault() : album.Label;
                    album.ReleaseDate = album.ReleaseDate.IsNullOrEmpty() ? firstResult.year : album.ReleaseDate;
                    album.CoverImageUrl = album.CoverImageUrl == null ? firstResult.cover_image : album.CoverImageUrl;
                    album.ThumbnailUrl = album.ThumbnailUrl == null ? firstResult.thumb : album.ThumbnailUrl;
                    album.DiscogResourceUrl = album.DiscogResourceUrl.IsNullOrEmpty() ? firstResult.master_url : album.DiscogResourceUrl;
                    //album.Description = album.Description == null ? firstResult.not : album.Description;
                    Task.Run(async () =>
                    {
                        fromDiscogsAlbum = await discogsClientAlbumModel.OnGet(firstResult!.master_url!);
                        Console.WriteLine("Asynchronous work completed.");
                    }).Wait();
                    if (fromDiscogsAlbum.id != 0)
                    {
                        album.Description = album.Description.IsNullOrEmpty() ? fromDiscogsAlbum.notes : album.Description;
                        var members = fromDiscogsAlbum.tracklist;
                        if (members != null)
                        {
                            List<Song> songs = new List<Song>();
                            foreach (var member in members)
                            {
                                Song song = new Song();
                                song.AlbumId = album.AlbumId;
                                song.UserId = album.UserId;
                                song.SongName = member.title;
                                song.DiscogResourceUrl = album.DiscogResourceUrl.IsNullOrEmpty() ? fromDiscogsAlbum.resource_url : album.DiscogResourceUrl;
                                song.Description = member.duration;
                                songs.Add(song);
                            }
                            album.Songs = songs;
                        }
                    }
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex);
                //return new Artist();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return new Artist();
            }
            return album;
        }

        public static AlbumsGalore.Server.Models.Artist PopulateArtistFromDiscogs(AlbumsGalore.Server.Models.Artist artist)
        {
            var httpClient = new HttpClient();
            DiscogsClientSearchArtistModel discogsClientSearchArtistModel = new DiscogsClientSearchArtistModel(httpClient);
            DiscogsClientArtistModel discogsClientArtistModel = new DiscogsClientArtistModel(httpClient);
            DiscogsClientMusicianModel discogsClientMusicianModel = new DiscogsClientMusicianModel(httpClient);
            var fromDiscogsArtistSearch = new DiscogsArtistSearch();
            var fromDiscogsArtist = new DiscogsArtist();
            try
            {
                Task.Run(async () =>
                {
                    fromDiscogsArtistSearch = await discogsClientSearchArtistModel.OnGet(artist.Name!);
                    Console.WriteLine("Asynchronous work completed.");
                }).Wait();
                var firstSearchResult = fromDiscogsArtistSearch.results.FirstOrDefault();
                if (firstSearchResult != null)
                {
                    Console.WriteLine("URL" + firstSearchResult.resource_url);
                    artist.DiscogResourceUrl = artist.DiscogResourceUrl.IsNullOrEmpty() ? firstSearchResult.resource_url : artist.DiscogResourceUrl;
                    artist.ThumbnailUrl = artist.ThumbnailUrl.IsNullOrEmpty() ? firstSearchResult.thumb : artist.ThumbnailUrl;
                    artist.CoverImageUrl = artist.CoverImageUrl == null ? firstSearchResult.cover_image : artist.CoverImageUrl;
                    Task.Run(async () =>
                    {
                        fromDiscogsArtist = await discogsClientArtistModel.OnGet(firstSearchResult.resource_url);
                        Console.WriteLine("Asynchronous work completed.");
                    }).Wait();
                    if (fromDiscogsArtist.id != 0)
                    {
                        artist.Description = artist.Description.IsNullOrEmpty() ? fromDiscogsArtist.profile : artist.Description;
                        var members = fromDiscogsArtist.members;
                        if (members != null)
                        {
                            List<Musician> musicians = new List<Musician>();
                            foreach (var member in members)
                            {
                                var fromDiscogsMusician = new DiscogsMusician();
                                Task.Run(async () =>
                                {
                                    fromDiscogsMusician = await discogsClientMusicianModel.OnGet(member.resource_url);
                                    Console.WriteLine("Asynchronous work completed.");
                                }).Wait();
                                if (fromDiscogsMusician.id != 0)
                                {
                                    Musician musician = new Musician();
                                    musician.MusicianName = member.name;
                                    musician.DiscogResourceUrl = artist.DiscogResourceUrl.IsNullOrEmpty() ? member.resource_url : artist.DiscogResourceUrl;
                                    musician.Description = fromDiscogsMusician.profile;
                                    musicians.Add(musician);
                                }
                            }
                            artist.Musicians = musicians;
                        }
                    }
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex);
                //return new Artist();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return new Artist();
            }
            return artist!;
        }

        public static List<T> ListDifference<T>(List<T> list1, List<T> list2, string itemOne, string itemTwo)
        {
            List<T> newList = new();
            Type listType = typeof(List<T>);

            foreach (T item in list1)
            {
                Type objectType = typeof(T);
                Debug.WriteLine("item " + item);
                MethodInfo methodOne = (MethodInfo)objectType.GetMember(itemOne)[0];
                MethodInfo methodTwo = (MethodInfo)objectType.GetMember(itemTwo)[0];
                int itemOneId = 0;
                int itemTwoId = 0;
                if (item != null)
                {
                    itemOneId = (int)methodOne.Invoke(item, null)!;
                    Debug.WriteLine("itemOneId " + itemOneId);
                    itemTwoId = (int)methodTwo.Invoke(item, null)!;
                    Debug.WriteLine("itemTwoId " + itemTwoId);
                }
                bool isFound = false;
                foreach (T item2 in list2)
                {
                    //int itemFirstId = 0;
                    //int itemSecondId = 0;
                    int itemFirstId = (int)methodOne.Invoke(item2, null)!;
                    Debug.WriteLine("itemOneId " + itemOneId);
                    int itemSecondId = (int)methodTwo.Invoke(item2, null)!;
                    Debug.WriteLine("itemTwoId " + itemTwoId);

                    if (itemOneId == itemFirstId && itemTwoId == itemSecondId)
                    {
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    newList.Add(item);
                }
            }

            return newList;
        }
    }

    public class GenericCompare<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> _expr { get; set; }
        public GenericCompare(Func<T, object> expr)
        {
            this._expr = expr;
        }
        public bool Equals(T x, T y)
        {
            var first = _expr.Invoke(x);
            var sec = _expr.Invoke(y);
            if (first != null && first.Equals(sec))
                return true;
            else
                return false;
        }
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

}
