namespace AlbumsGalore.Server.Models.CustomModels.DiscogsAlbums
{

    public class DiscogsAlbum
    {
        public int id { get; set; }
        public int main_release { get; set; }
        public int most_recent_release { get; set; }
        public string resource_url { get; set; }
        public string uri { get; set; }
        public string versions_url { get; set; }
        public string main_release_url { get; set; }
        public string most_recent_release_url { get; set; }
        public int num_for_sale { get; set; }
        public float lowest_price { get; set; }
        public Image[] images { get; set; }
        public string[] genres { get; set; }
        public string[] styles { get; set; }
        public int year { get; set; }
        public Tracklist[] tracklist { get; set; }
        public Artist[] artists { get; set; }
        public string title { get; set; }
        public string notes { get; set; }
        public string data_quality { get; set; }
        public Video[] videos { get; set; }
    }

    public class Image
    {
        public string type { get; set; }
        public string uri { get; set; }
        public string resource_url { get; set; }
        public string uri150 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Tracklist
    {
        public string position { get; set; }
        public string type_ { get; set; }
        public string title { get; set; }
        public string duration { get; set; }
    }

    public class Artist
    {
        public string name { get; set; }
        public string anv { get; set; }
        public string join { get; set; }
        public string role { get; set; }
        public string tracks { get; set; }
        public int id { get; set; }
        public string resource_url { get; set; }
    }

    public class Video
    {
        public string uri { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public bool embed { get; set; }
    }


}
