using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Style
{
    public int StyleId { get; set; }

    public string StyleName { get; set; } = null!;

    public DateTime DateInserted { get; set; }

    public virtual ICollection<AlbumStyle> AlbumStyles { get; set; } = new List<AlbumStyle>();
}
