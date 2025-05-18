using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<UserRolePermissionAssociation> UserRolePermissionAssociations { get; set; } = new List<UserRolePermissionAssociation>();

    public virtual ICollection<UserRolesAssociation> UserRolesAssociations { get; set; } = new List<UserRolesAssociation>();
}
