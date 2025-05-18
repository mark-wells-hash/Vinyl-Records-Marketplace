using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class MediaType
{
    public int MediaTypeId { get; set; }

    public string? MediaName { get; set; }

    public DateTime DateInserted { get; set; }

    public virtual ICollection<AlbumMediaType> AlbumMediaTypes { get; set; } = new List<AlbumMediaType>();
}
