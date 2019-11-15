using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Pgh.Services.Authentification.Helpers
{
    public class AuthModelDto
    {
        [JsonProperty("Token_Attribute")]
        public JwtToken Token { get; set; }
        [JsonProperty("Filiale_Utilisateur")]
        public string UserFiliale { get; set; }
        [JsonProperty("Nom_Utilisateur")]
        public string UserName { get; set; }
        [JsonProperty("Mail_Utilisateur")]
        public string UserMail { get; set; }
        [JsonProperty("Code_Utilisateur")]
        public string UserLogin { get; set; }
        [JsonProperty("Nom_Application")]
        public string Application { get; set; }
        [JsonProperty("Code_Application")]
        public string AppCode { get; set; }
        [JsonProperty("Role_Utilisateur")]
        public string RoleUser { get; set; }
        [JsonProperty("PhoneExterne")]
        public string PhoneExterne { get; set; }
        [JsonProperty("PhoneInterne")]
        public string PhoneInterne { get; set; }
        [JsonProperty("Liste_des_permissions")]
        public List<MenuPermission> MenuPermissions { get; set; }

    }

    public class MenuPermission
    {
        [JsonProperty("Menu_Nom")]
        public string MenuName { get; set; }
        [JsonProperty("Menu_Url")]
        public string MenuUrl { get; set; }
        [JsonProperty("Menu_ID")]
        public Guid MenuId { get; set; }
        [JsonProperty("Menu_Parent_ID")]
        public Guid? MenuParentId { get; set; }
        [JsonProperty("Détails_des_permissions")]
        public List<PermissionDetail> PermissionDetails { get; set; }
    }

    public class PermissionDetail
    {
        [JsonProperty("Permission_Nom")]
        public string PermissionName { get; set; }
        [JsonProperty("Permission_ID")]
        public Guid PermissionId { get; set; }

        [JsonProperty("Groupe_Nom")]
        public string GroupeName { get; set; }
        [JsonProperty("Groupe_ID")]
        public Guid? GroupeId { get; set; }
    }

}