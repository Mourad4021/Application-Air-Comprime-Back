using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.Models
{
    public class Menus
    {
        [Key]
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDisplayName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuUrl { get; set; }
        public bool MenuState { get; set; }
        public Guid? FkMenuId { get; set; }
        public Guid? FkAppId { get; set; }
        

        public virtual Applications FkApp { get; set; }
        public virtual Menus FkMenu { get; set; }
        public virtual ICollection<AffRoleGroupMenus> AffRoleGroupMenus { get; set; }
        public virtual ICollection<AffRolesUsersMenus> AffRolesUsersMenus { get; set; }
        public virtual ICollection<Menus> InverseFkMenu { get; set; }
        
    }
}