namespace AlbumsGalore.Server.Models.CustomModels.DiscogsMusicians
{

    public class DiscogsMusician
    {
        public string name { get; set; }
        public int id { get; set; }
        public string resource_url { get; set; }
        public string uri { get; set; }
        public string releases_url { get; set; }
        public Image[] images { get; set; }
        public string realname { get; set; }
        public string profile { get; set; }
        public string[] urls { get; set; }
        public string[] namevariations { get; set; }
        public Alias[] aliases { get; set; }
        public Group[] groups { get; set; }
        public string data_quality { get; set; }
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

    public class Alias
    {
        public int id { get; set; }
        public string name { get; set; }
        public string resource_url { get; set; }
    }

    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string resource_url { get; set; }
        public bool active { get; set; }
    }
}