using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.Models
{
    public class Roles
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleState { get; set; }

        public virtual ICollection<AffRolePermissions> AffRolePermissions { get; set; }

        public virtual ICollection<AffRoleGroupMenus> AffRoleGroupMenus { get; set; }
        public virtual ICollection<AffRolesUsersMenus> AffRolesUsersMenus { get; set; }

    }
}