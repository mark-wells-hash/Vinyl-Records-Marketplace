using System;
using System.Collections.Generic;

namespace AlbumsGalore.Server.Models;

public partial class UserPermission
{
    public int PermissionsId { get; set; }

    public string? PermissionsName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<UserRolePermissionAssociation> UserRolePermissionAssociations { get; set; } = new List<UserRolePermissionAssociation>();
}
