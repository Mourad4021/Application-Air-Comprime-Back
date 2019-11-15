using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pgh.Auth.Model.ModelViews.Dto
{
    
    public class RoleDtoForCreate
    {
        [Required]
        [JsonProperty("Role Name")]
        public string RoleName { get; set; }
        [Required]
        [JsonProperty("Role Display Name")]
        public string RoleDisplayName { get; set; }
        [JsonProperty("Role Description")]
        public string RoleDescription { get; set; }
        [JsonProperty("Role State")]
        public bool RoleState { get; set; }
    }

    public class RoleDtoForReadUpdate
    {
        [Required]
        [JsonProperty("RoleID")]
        public Guid RoleId { get; set; }
        [Required]
        [JsonProperty("RoleName")]
        public string RoleName { get; set; }
        [JsonProperty("RoleDisplayName")]
        public string RoleDisplayName { get; set; }
        [JsonProperty("Role Description")]
        public string RoleDescription { get; set; }
        [JsonProperty("Role State")]
        public bool RoleState { get; set; }
    }
}