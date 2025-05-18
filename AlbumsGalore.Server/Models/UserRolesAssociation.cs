using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserRolesAssociation
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual UserRole Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
