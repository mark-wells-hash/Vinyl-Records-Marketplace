using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumMediaType
{
    public int AlbumMediaTypeId { get; set; }

    public int AlbumId { get; set; }

    public int MediaTypeId { get; set; }

    public DateTime? DatInserted { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual MediaType MediaType { get; set; } = null!;
}
