using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public int? ArtistId { get; set; }

    public string? AlbumName { get; set; }

    public string? Description { get; set; }

    public DateTime? DateInserted { get; set; }

    public int? UserId { get; set; }

    public int? AlbumConditionTypeId { get; set; }

    public string? ReleaseDate { get; set; }

    public string? DiscogResourceUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Label { get; set; }

    public int? AlbumStatusId { get; set; }

    public decimal? Price { get; set; }

    public virtual AlbumConditionType? AlbumConditionType { get; set; }

    public virtual ICollection<AlbumGenre> AlbumGenres { get; set; } = new List<AlbumGenre>();

    public virtual ICollection<AlbumMediaType> AlbumMediaTypes { get; set; } = new List<AlbumMediaType>();

    public virtual ICollection<AlbumMusician> AlbumMusicians { get; set; } = new List<AlbumMusician>();

    public virtual AlbumStatus? AlbumStatus { get; set; }

    public virtual ICollection<AlbumStyle> AlbumStyles { get; set; } = new List<AlbumStyle>();

    public virtual Artist? Artist { get; set; }

    public virtual ICollection<SalesOrdersItem> SalesOrdersItems { get; set; } = new List<SalesOrdersItem>();

    public virtual ICollection<SalesShoppingCart> SalesShoppingCarts { get; set; } = new List<SalesShoppingCart>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    public virtual User? User { get; set; }
}
