using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Song
{
    public int SongId { get; set; }

    public int? AlbumId { get; set; }

    public string? SongName { get; set; }

    public string? Description { get; set; }

    public string? DiscogResourceUrl { get; set; }

    public DateTime? DateInserted { get; set; }

    public int? UserId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual User? User { get; set; }
}
