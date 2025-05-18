using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class SalesStatus
{
    public int SalesStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public DateTime? DateInserted { get; set; }

    public virtual ICollection<SalesShoppingCart> SalesShoppingCarts { get; set; } = new List<SalesShoppingCart>();
}
