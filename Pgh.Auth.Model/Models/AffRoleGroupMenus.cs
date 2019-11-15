using System;

namespace Pgh.Auth.Model.Models
{
    public class AffRoleGroupMenus
    {
        public Guid RoleId { get; set; }
        public Guid GrpId { get; set; }
        public Guid MenuId { get; set; }
       

        public virtual Roles Role { get; set; }
        public virtual Groupes Groupe { get; set; }
        public virtual Menus Menu { get; set; }
        

    }
}