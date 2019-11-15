using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.ModelViews.Dto
{
    public class ApplicationDtoCreate
    {
        [Required, MaxLength(15)]
        [JsonProperty("Application Code")]
        public string AppCode { get; set; }
        [Required, MaxLength(40)]
        [JsonProperty("Application Name")]
        public string AppName { get; set; }
        [Required, MaxLength(50)]
        [JsonProperty("Application Display Name")]
        public string AppDisplayName { get; set; }
        [JsonProperty("Application Description")]
        public string AppDescription { get; set; }
        [JsonProperty("Application State")]
        public bool AppState { get; set; }
    }

    public class ApplicationDtoRead
    {
        [Required]
        [JsonProperty("ApplicationID")]
        public Guid AppId { get; set; }
        [JsonProperty("ApplicationCode")]
        public string AppCode { get; set; }
        [JsonProperty("ApplicationName")]
        public string AppName { get; set; }
        [JsonProperty("ApplicationDisplayName")]
        public string AppDisplayName { get; set; }
        //Might hide this informations
        [JsonProperty("ApplicationDescription")]
        public string AppDescription { get; set; }
        [JsonProperty("ApplicationState")]
        public bool AppState { get; set; }
    }

    public class ApplicationUsersDto
    {
        [Required]
        [JsonProperty("ApplicationID")]
        public Guid AppId { get; set; }
        [JsonProperty("ApplicationCode")]
        public string AppCode { get; set; }
        [JsonProperty("ApplicationName")]
        public string AppName { get; set; }
        [JsonProperty("ApplicationDisplayName")]
        public string AppDisplayName { get; set; }
        //Might hide this informations
        [JsonProperty("ApplicationDescription")]
        public string AppDescription { get; set; }
        [JsonProperty("ApplicationState")]
        public bool AppState { get; set; }

        [JsonProperty("ListeUtilisateurs")]
        public IEnumerable<ApplicationUsersDtoRead> Users { get; set; } = new List<ApplicationUsersDtoRead>();
    }

    public class AppUsersDto
    {
        [Required]
        [JsonProperty("User ID")]
        public Guid IdUser { get; set; }
        [Required]
        [JsonProperty("User Password")]
        public string Password { get; set; }
    }

    public class ApplicationUsersDtoRead
    {
        [Required]
        [JsonProperty("UserID")]
        public Guid UsersId { get; set; }
        [Required, MaxLength(8)]
        [JsonProperty("UserCIN")]
        public string UsersCode { get; set; }
        [JsonProperty("UserName")]
        public string UsersName { get; set; }
        [JsonProperty("UserState")]
        public string UsersState { get; set; }
        [JsonProperty("UserMail")]
        public string UsersMail { get; set; }
        [JsonProperty("UserMailIntern")]
        public string UsersMailIntern { get; set; }
        [JsonProperty("UserPostName")]
        public string UsersPosteName { get; set; }
        [JsonProperty("UserOfficeNumber")]
        public string UsersPhoneNumber { get; set; }
        [JsonProperty("UserPersonalNumber")]
        public string UsersPersonalNumber { get; set; }
        [JsonProperty("User Gender")]
        public string UsersGenderCode { get; set; }
        [JsonProperty("User Subsidiary Name")]
        public string UsersFilialeName { get; set; }
        [JsonProperty("User Subsidiary Code")]
        public string UsersFilialeCode { get; set; }
        [JsonProperty("User Birth Date")]
        public DateTime UsersBirthDate { get; set; }
        [JsonProperty("User Join Date")]
        public DateTime UsersJoinDate { get; set; }
        [JsonProperty("User Leave Date")]
        public DateTime UsersDateLeave { get; set; }
        [JsonProperty("User Password")]
        public string Password { get; set; }
    }

}