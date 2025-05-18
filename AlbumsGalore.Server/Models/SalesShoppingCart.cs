using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesShoppingCart
{
    public int SalesShoppingCartId { get; set; }

    public int ItemId { get; set; }

    public int UserId { get; set; }

    public int SalesStatusId { get; set; }

    public DateTime? DateInserted { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual Album Item { get; set; } = null!;

    public virtual ICollection<SalesOffer> SalesOffers { get; set; } = new List<SalesOffer>();

    public virtual SalesStatus SalesStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
