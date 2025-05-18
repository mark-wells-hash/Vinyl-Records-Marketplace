using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesOfferStatus
{
    public int SalesOfferStatusId { get; set; }

    public string OfferStatusName { get; set; } = null!;

    public string? OfferStatusDescription { get; set; }

    public DateTime? DateInserted { get; set; }

    public virtual ICollection<SalesOffer> SalesOffers { get; set; } = new List<SalesOffer>();
}
