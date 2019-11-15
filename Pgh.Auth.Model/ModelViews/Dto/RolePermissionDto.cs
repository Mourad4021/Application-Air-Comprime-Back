using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.ModelViews.Dto
{

    public class RolePermissionDtoReadUpdate
    {
        [Required]
        [JsonProperty("Role ID")]
        public Guid RoleId { get; set; }
        [JsonProperty("Role Name")]
        public string RoleName { get; set; }
        [JsonProperty("Role Display Name")]
        public string RoleDisplayName { get; set; }
        [JsonProperty("Role Description")]
        public string RoleDescription { get; set; }
        [JsonProperty("Role State")]
        public bool RoleState { get; set; }


        [JsonProperty("Liste Permissions")]
        public virtual IList<PermissionDtoReadUpdate> PermissionList { get; set; } =
            new List<PermissionDtoReadUpdate>();
    }

    public class RoleUsersMenusDto
    {
        public RoleDtoForReadUpdate Role { get; set; }
        public UsersDtoForRead User { get; set; }
        public MenusDtoRead Menus{ get; set; }
    }

    public class RoleUsersMenusDtoCreate
    {
        [JsonProperty("User ID")]
        public Guid UserId { get; set; }
        [JsonProperty("Menu ID")]
        public Guid MenuId { get; set; }
        [JsonProperty("Role ID")]
        public Guid RoleId { get; set; }

        
    }

    public class RoleUsersMenusDtoGetDelete
    {
        [JsonProperty("Role ID")]
        public Guid? RoleId { get; set; }
        [JsonProperty("User ID")]
        public Guid? UserId { get; set; }
        [JsonProperty("Menu ID")]
        public Guid? MenuId { get; set; }
    }

    public class RoleUsersMenusDtoUpdate
    {
        [JsonProperty("Role ID")]
        public Guid? RoleId { get; set; }
        [JsonProperty("User ID")]
        public Guid? UserId { get; set; }
        [JsonProperty("Menu ID")]
        public Guid? MenuId { get; set; }
    }



    /// <summary>
    /// Groupe Menu Role Affectation Used class in controller
    /// </summary>
    public class RoleGroupesMenusDto
    {
        public RoleDtoForReadUpdate Role { get; set; }
        public GroupeDtoReadUpdate Groupe { get; set; }
        public MenusDtoRead Menu { get; set; }
    }

    public class RoleGroupesMenusDtoCreate
    {
        [JsonProperty("Role ID")]
        public Guid? RoleId { get; set; }
        [JsonProperty("Groupe ID")]
        public Guid? GroupeId { get; set; }
        [JsonProperty("Menu ID")]
        public Guid? MenuId { get; set; }
    }

    public class RoleGroupesMenusDtoGetDelete
    {
        [JsonProperty("Role ID")]
        public Guid? RoleId { get; set; }
        [JsonProperty("Groupe ID")]
        public Guid? GroupeId { get; set; }
        [JsonProperty("Menu ID")]
        public Guid? MenuId { get; set; }
    }

}