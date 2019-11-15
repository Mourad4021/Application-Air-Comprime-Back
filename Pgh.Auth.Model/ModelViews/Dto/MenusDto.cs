using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pgh.Auth.Model.ModelViews.Dto
{
    public class MenusDtoCreate
    {
        [Required]
        [JsonProperty("Menu Name")]
        public string MenuName { get; set; }
        [Required]
        [JsonProperty("Menu Display Name")]
        public string MenuDisplayName { get; set; }
        [JsonProperty("Menu Description")]
        public string MenuDescription { get; set; }
        [Required]
        [JsonProperty("Menu Url")]
        public string MenuUrl { get; set; }
        [JsonProperty("Menu State")] 
        public bool MenuState { get; set; }
        [Required]
        [JsonProperty("Application ID")]
        public Guid ApplicationAppId { get; set; }
        [JsonProperty("Menu Children")]
        public List<MenusDtoCreate> SubMenus { get; set; } =  new List<MenusDtoCreate>();

    }

    public class MenusDtoCreateList
    {
        [JsonProperty("Menu Parent ID")]
        public Guid? ParentId { get; set; }
        [Required]
        [JsonProperty("Liste des menus")]
        public MenusDtoCreate LiMenu { get; set; }
    }

    public class MenusDtoRead
    {
        [Required]
        [JsonProperty("Menu ID")]
        public Guid MenuId { get; set; }
        [Required]
        [JsonProperty("Menu Name")]
        public string MenuName { get; set; }
        [JsonProperty("Menu Display Name")]
        public string MenuDisplayName { get; set; }
        [JsonProperty("Menu description")]
        public string MenuDescription { get; set; }
        [JsonProperty("Menu Url")]
        public string MenuUrl { get; set; }
        [JsonProperty("Menu State")]
        public bool MenuState { get; set; }
        [JsonProperty("Menu Parent ID")]
        public Guid? MenuParentId { get; set; }
        [JsonProperty("Menu Parent Name")]
        public string MenuParentName { get; set; }
        [JsonProperty("Application Name")]
        public string AppName { get; set; }
        [JsonProperty("Application ID")]
        public Guid AppId { get; set; }
    }

    public class MenusDtoUpdate
    {
        [Required]
        [JsonProperty("Menu ID")]
        public Guid MenuId { get; set; }
        [Required]
        [JsonProperty("Menu Name")]
        public string MenuName { get; set; }
        [JsonProperty("Menu Display Name")]
        public string MenuDisplayName { get; set; }
        [Required]
        [JsonProperty("Menu Description")]
        public string MenuDescription { get; set; }
        [JsonProperty("Menu Url")]
        public string MenuUrl { get; set; }
        [JsonProperty("Menu State")]
        public bool MenuState { get; set; }
        [JsonProperty("Menu Parent ID")]
        public Guid? MenuParentId { get; set; }
        [JsonProperty("Application ID")]
        public Guid AppId { get; set; }
    }
}