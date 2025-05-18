using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumStatus
{
    public int AlbumStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime DateInserted { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
