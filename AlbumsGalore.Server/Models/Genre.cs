using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public DateTime DateInserted { get; set; }

    public virtual ICollection<AlbumGenre> AlbumGenres { get; set; } = new List<AlbumGenre>();
}
