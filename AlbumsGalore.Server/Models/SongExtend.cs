namespace AlbumsGalore.Server.Models
{
    public class SongExtend: Song
    {
        public SongExtend() { }

        public SongExtend(int id) { }

        public string? AlbumName { get; set; }

        public string? ArtistName { get; set;}

        public string? ConditionDescription { get; set; }

        public string? ConditionName { get; set; }
    }
}
