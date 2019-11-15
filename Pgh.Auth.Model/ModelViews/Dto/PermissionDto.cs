using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pgh.Auth.Model.ModelViews.Dto
{
    
    public class PermissionDtoReadUpdate
    {
        [Required]
        [JsonProperty("Permission ID")]
        public Guid PermId { get; set; }
        [JsonProperty("Permission Name")]
        public string PermName { get; set; }
        [JsonProperty("Permission Display Name")]
        public string PermDisplayName { get; set; }
        [JsonProperty("Permission Description")]
        public string PermDescription { get; set; }
        [JsonProperty("Permission State")]
        public bool PermState { get; set; }
    }

    public class PermissionDtoCreate
    {
        [Required]
        [JsonProperty("Permission Name")]
        public string PermName { get; set; }
        [Required]
        [JsonProperty("Permission Display Name")]
        public string PermDisplayName { get; set; }
        [JsonProperty("Permission Description")]
        public string PermDescription { get; set; }
        [JsonProperty("Permission State")]
        public bool PermState { get; set; }
    }

}