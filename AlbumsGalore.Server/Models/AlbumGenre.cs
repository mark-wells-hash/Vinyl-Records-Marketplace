using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumGenre
{
    public int AlbumGenreId { get; set; }

    public int AlbumId { get; set; }

    public int GenreId { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
