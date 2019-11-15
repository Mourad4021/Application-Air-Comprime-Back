using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.Models
{
    public class Permissions
    {
        [Key]
        public Guid PermId { get; set; }
        public string PermName { get; set; }
        public string PermDisplayName { get; set; }
        public string PermDescription { get; set; }
        public bool PermState { get; set; }


        public virtual ICollection<AffRolePermissions> AffRolePermissions { get; set; }
    }
}