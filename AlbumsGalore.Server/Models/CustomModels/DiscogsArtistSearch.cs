namespace AlbumsGalore.Server.Models.CustomModels.DiscogsArtists
{
    //public class DiscogsArtistSearch
    //{
    public class DiscogsArtistSearch
    {
        public Pagination pagination { get; set; }
        public Result[] results { get; set; }
    }

    public class Pagination
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int per_page { get; set; }
        public int items { get; set; }
        public Urls urls { get; set; }
    }

    public class Urls
    {
    }

    public class Result
    {
        public int id { get; set; }
        public string type { get; set; }
        public object master_id { get; set; }
        public object master_url { get; set; }
        public string uri { get; set; }
        public string title { get; set; }
        public string thumb { get; set; }
        public string cover_image { get; set; }
        public string resource_url { get; set; }
    }

    //}
}
