using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string City { get; set; } = null!;

    public string StateProvince { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ZipPostalCode { get; set; } = null!;

    public DateTime DateInserted { get; set; }

    public decimal? Lat { get; set; }

    public decimal? Long { get; set; }

    public int? AddressTypeId { get; set; }

    public bool? IsActive { get; set; }

    public virtual AddressType? AddressType { get; set; }

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
