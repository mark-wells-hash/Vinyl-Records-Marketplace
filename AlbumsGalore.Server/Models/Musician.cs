using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Musician
{
    public int MusicianId { get; set; }

    public int? ArtistId { get; set; }

    public string? MusicianName { get; set; }

    public string? Description { get; set; }

    public string? DiscogResourceUrl { get; set; }

    public DateTime? DateInserted { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<AlbumMusician> AlbumMusicians { get; set; } = new List<AlbumMusician>();

    public virtual Artist? Artist { get; set; }

    public virtual User? User { get; set; }
}
