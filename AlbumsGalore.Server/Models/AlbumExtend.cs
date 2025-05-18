using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AlbumsGalore.Server.Models
{
    public class AlbumExtend: Album
    {
        public AlbumExtend() { }

        public AlbumExtend(int id) {
          
        }
        public string? MediaName { get; set; }

        public string? ConditionName { get; set;}

        public string? ConditionDescription { get; set; }

        public string? ArtistName { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public string? GenreString { get => GenerateGenreString(Genres); set { } }

        public List<MediaType> MediaTypes { get; set; } = new List<MediaType>();

        public string? MediaTypeString { get => GenerateMediaTypeString(MediaTypes); set { } }

        public List<Musician> Musicians { get; set; } = new List<Musician>();

        public string? MusicianString { get => GenerateMusicianString(Musicians); set { } }

        public string? StatusName { get; set; }

        public List<Style> Styles { get; set; } = new List<Style>();

        public string? StyleString { get => GenerateStyleString(Styles); set { } }

        private string GenerateGenreString(List<Genre> genres)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < genres.Count; i++)
            {
                if (i == genres.Count - 1)
                {
                    strBuilder.Append(genres[i].GenreName);
                }
                else
                {
                    strBuilder.Append(genres[i].GenreName + ", ");
                }
            }
            return strBuilder.ToString();
        }

        private string GenerateMediaTypeString(List<MediaType> medias)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < medias.Count; i++)
            {
                if (i == medias.Count - 1)
                {
                    strBuilder.Append(medias[i].MediaName);
                }
                else
                {
                    strBuilder.Append(medias[i].MediaName + ", ");
                }
            }
            return strBuilder.ToString();
        }

        private string GenerateMusicianString(List<Musician> musicians)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < musicians.Count; i++)
            {
                if (i == musicians.Count - 1)
                {
                    strBuilder.Append(musicians[i].MusicianName);
                }
                else
                {
                    strBuilder.Append(musicians[i].MusicianName + ", ");
                }
            }
            return strBuilder.ToString();
        }

        private string GenerateStyleString(List<Style> styles)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < styles.Count; i++)
            {
                if (i == styles.Count - 1)
                {
                    strBuilder.Append(styles[i].StyleName);
                }
                else
                {
                    strBuilder.Append(styles[i].StyleName + ", ");
                }
            }
            return strBuilder.ToString();
        }
    }
}
