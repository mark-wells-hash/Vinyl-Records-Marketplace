using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class AddressType
{
    public int AddressTypeId { get; set; }

    public string AddressTypeName { get; set; } = null!;

    public string? AddressDescription { get; set; }

    public DateTime DateInserted { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
