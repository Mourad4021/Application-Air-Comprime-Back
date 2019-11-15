using System;

namespace Pgh.Auth.Model.Models
{
    public class AffRolesUsersMenus
    {
        public Guid RoleId { get; set; }
        public Guid UsersId { get; set; }
        public Guid MenuId { get; set; }


        public virtual Roles Role { get; set; }
        public virtual Users Users { get; set; }
        public virtual Menus Menu { get; set; }
    }
}