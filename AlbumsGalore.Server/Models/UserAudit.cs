using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserAudit
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string? ActivityType { get; set; }

    public DateTime TimeStamp { get; set; }

    public string? Description { get; set; }

    public virtual User? User { get; set; }
}
