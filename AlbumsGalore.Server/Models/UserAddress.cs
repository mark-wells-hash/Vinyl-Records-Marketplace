using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserAddress
{
    public int UserAddressesId { get; set; }

    public int UserId { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
