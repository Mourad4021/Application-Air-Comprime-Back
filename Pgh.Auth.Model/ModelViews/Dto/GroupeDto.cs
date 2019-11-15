using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pgh.Auth.Model.ModelViews.Dto
{
    
    public class GroupeDtoCreate
    {
        [Required]
        [JsonProperty("Groupe Name")]
        public string GrpName { get; set; }
        [Required]
        [JsonProperty("Groupe Display Name")]
        public string GrpDisplayName { get; set; }
        [JsonProperty("Groupe Description")]
        public string GrpDescription { get; set; }
        [JsonProperty("Groupe State")]
        public bool GrpState { get; set; }
        [Required]
        [JsonProperty("Application ID")]
        public Guid FkAppId { get; set; }
    }

    public class GroupeDtoReadUpdate
    {
        [Required]
        [JsonProperty("Groupe ID")]
        public Guid GrpId { get; set; }
        [JsonProperty("Groupe Name")]
        public string GrpName { get; set; }
        [JsonProperty("Groupe Display Name")]
        public string GrpDisplayName { get; set; }
        [JsonProperty("Groupe Description")]
        public string GrpDescription { get; set; }
        [JsonProperty("Groupe State")]
        public bool GrpState { get; set; }
        [JsonProperty("Application Name")]
        public string AppName { get; set; }

        [Required]
        [JsonProperty("Application ID")]
        public Guid AppGuid { get; set; }
    }

    

    public class GroupeUserDtoRead
    {
        [Required]
        [JsonProperty("Groupe Name")]
        public string GrpName { get; set; }
        [JsonProperty("Groupe Display Name")]
        public string GrpDisplayName { get; set; }
        [JsonProperty("Groupe Description")]
        public string GrpDescription { get; set; }
        [JsonProperty("Groupe State")]
        public bool GrpState { get; set; }
        [JsonProperty("Application Name")]
        public string AppName { get; set; }

        [JsonProperty("Liste Utilisateurs")]
        public List<UsersDtoForRead> Users { get; set; } = new List<UsersDtoForRead>();
    }
    
}