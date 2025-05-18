using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserPasswordReset
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string TokenValue { get; set; } = null!;

    public DateTime? ExpiryDate { get; set; }

    public virtual User? User { get; set; }
}
