using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserRolePermissionAssociation
{
    public int RolePermissionId { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual UserPermission Permission { get; set; } = null!;

    public virtual UserRole Role { get; set; } = null!;
}
