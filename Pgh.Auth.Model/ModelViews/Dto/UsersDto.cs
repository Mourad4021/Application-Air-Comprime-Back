using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pgh.Auth.Model.ModelViews.Dto
{

    public class UsersDtoForCreation
    {
        [Required]
        [JsonProperty("User Code")]
        [MaxLength(8, ErrorMessage = "Code length can't be more than 8.")]
        public string UsersCode { get; set; }
        [Required]
        [JsonProperty("User First Name")]
        public string UsersName { get; set; }
        [Required]
        [JsonProperty("User Last Name")]
        public string UsersLastName { get; set; }
        [JsonProperty("User State")]
        public bool UsersState { get; set; }
        [JsonProperty("User Mail")]
        public string UsersMail { get; set; }
        [JsonProperty("User Mail Interne")]
        public string UsersMailIntern { get; set; }
        [JsonProperty("User Post Name")]
        public string UsersPosteName { get; set; }
        [JsonProperty("User Office Number")]
        public string UsersPhoneNumber { get; set; }
        [JsonProperty("User Personal Number")]
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
        [JsonProperty("User collaborators")]
        public List<UsersDtoForCreation> SubUsers { get; set; } = new List<UsersDtoForCreation>();
    }

    public class UsersDtoForRead
    {
        [Required]
        [JsonProperty("UserID")]
        public Guid UsersId { get; set; }
        [Required]
        [JsonProperty("User Code")]
        public string UsersCode { get; set; }
        [JsonProperty("User Full Name")]
        public string UsersFullName { get; set; }
        [JsonProperty("User State")]
        public string UsersState { get; set; }
        [JsonProperty("User Mail")]
        public string UsersMail { get; set; }
        [JsonProperty("User Interne mail")]
        public string UsersMailIntern { get; set; }
        [JsonProperty("User post Name")]
        public string UsersPosteName { get; set; }
        [JsonProperty("User Office Number")]
        public string UsersPhoneNumber { get; set; }
        [JsonProperty("User Personal Number")]
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
        [JsonProperty("User Supervisor Code")]
        public string UsersSupCode { get; set; }
        [JsonProperty("User Supervisor Name")]
        public string SupFullName { get; set; }
    }

    public class UsersDtoForUpdate
    {
        [Required]
        [JsonProperty("User ID")]
        public Guid UsersId { get; set; }
        [Required]
        [JsonProperty("User Code")]
        public string UsersCode { get; set; }
        [JsonProperty("User First Name")]
        public string UsersName { get; set; }
        [JsonProperty("User Last Name")]
        public string UsersLastName { get; set; }
        [JsonProperty("User State")]
        public string UsersState { get; set; }
        [JsonProperty("User Mail")]
        public string UsersMail { get; set; }
        [JsonProperty("User Interne mail")]
        public string UsersMailIntern { get; set; }
        [JsonProperty("User post Name")]
        public string UsersPosteName { get; set; }
        [JsonProperty("User Office Number")]
        public string UsersPhoneNumber { get; set; }
        [JsonProperty("User Personal Number")]
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
        [JsonProperty("Supervisor ID")]
        public Guid? FkUsersId { get; set; }
    }

    public class UsersDtoCreateList
    {
        [JsonProperty("Parent ID")]
        public Guid? ParentId { get; set; }

        [JsonProperty("Users List")]
        public UsersDtoForCreation LiUsers { get; set; }
    }

}