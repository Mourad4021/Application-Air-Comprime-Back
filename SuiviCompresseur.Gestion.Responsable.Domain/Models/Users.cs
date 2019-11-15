using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Models
{
    public class Users
    {
        [Key]
        public Guid UsersId { get; set; }
        [StringLength(8)]
        public string UsersCode { get; set; }
        [StringLength(50)]
        public string UsersName { get; set; }
        [StringLength(50)]
        public string UsersLastName { get; set; }
        public bool UsersState { get; set; }
        [StringLength(80)]
        public string UsersMail { get; set; }
        [StringLength(80)]
        [RegularExpression("^[a-zA-Z ]*/[a-zA-Z]*/[a-zA-Z]*$", ErrorMessage = "E-mail_Lotus is not valid")]
        ///////////////////
        public string UsersMailIntern { get; set; }
        public string UsersPosteName { get; set; }
        public string UsersPhoneNumber { get; set; }
        public string UsersPersonalNumber { get; set; }
        public string UsersGenderCode { get; set; }
      

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UsersBirthDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UsersJoinDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UsersDateLeave { get; set; }
        public Guid? FkUsersId { get; set; }


      public Guid? FilialeID { get; set; }
       //public Filiale Filiale { get; set; }

        public virtual Users FkUsers { get; set; }


        //public virtual ICollection<AffApplicationUsers> AffApplicationUsers { get; set; }

        //public virtual ICollection<AffGroupUsers> AffGroupUsers { get; set; }

        //public virtual ICollection<AffRolesUsersMenus> AffRolesUsersMenus { get; set; }

        public virtual ICollection<Users> InverseFkUsers { get; set; }
    }
}
