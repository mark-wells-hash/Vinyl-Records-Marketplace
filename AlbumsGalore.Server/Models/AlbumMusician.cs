using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumMusician
{
    public int AlbumMusiciansId { get; set; }

    public int AlbumId { get; set; }

    public int MusicianId { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Musician Musician { get; set; } = null!;
}
