using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumStyle
{
    public int AlbumStyleId { get; set; }

    public int AlbumId { get; set; }

    public int StyleId { get; set; }

    public DateTime DateInserted { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
