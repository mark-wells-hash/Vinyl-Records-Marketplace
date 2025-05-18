using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AlbumConditionType
{
    public int AlbumConditionTypeId { get; set; }

    public string? ConditionName { get; set; }

    public string? ConditionDescription { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
