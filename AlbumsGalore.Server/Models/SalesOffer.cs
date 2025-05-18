using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesOffer
{
    public int SalesOffersId { get; set; }

    public int SalesShoppingCartId { get; set; }

    public int BuyerId { get; set; }

    public int SellerId { get; set; }

    public decimal OfferAmount { get; set; }

    public int? SalesOfferStatusId { get; set; }

    public int? InstigatedByUserId { get; set; }

    public DateTime? DateInserted { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual SalesOfferStatus? SalesOfferStatus { get; set; }

    public virtual SalesShoppingCart SalesShoppingCart { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;
}
