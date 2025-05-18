using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? DiscogResourceUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public DateTime DateInserted { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Musician> Musicians { get; set; } = new List<Musician>();

    public virtual User? User { get; set; }
}
