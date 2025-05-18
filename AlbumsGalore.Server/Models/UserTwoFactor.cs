using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserTwoFactor
{
    public int TwoFactorId { get; set; }

    public int UserId { get; set; }

    public string? Method { get; set; }

    public string? VerificationCode { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual User User { get; set; } = null!;
}
