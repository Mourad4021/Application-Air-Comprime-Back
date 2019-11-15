using System;
using System.Collections;
using System.Collections.Generic;

namespace Pgh.Auth.Model.Models
{
    public class AffRolePermissions
    {
        public Guid RoleId { get; set; }
        public Guid PermId { get; set; }
        
        
        public virtual Roles Role { get; set; }
        public virtual Permissions Permission { get; set; }

    }
}