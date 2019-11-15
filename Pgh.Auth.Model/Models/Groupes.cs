using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.Models
{
    public class Groupes
    {
        [Key]
        public Guid GrpId { get; set; }
        public string GrpName { get; set; }
        public string GrpDisplayName { get; set; }
        public string GrpDescription { get; set; }
        public bool GrpState { get; set; }
        public Guid? FkAppId { get; set; }

        public virtual Applications FkApp { get; set; }

        public virtual ICollection<AffGroupUsers> AffGroupUsers { get; set; }

        public virtual ICollection<AffRoleGroupMenus> AffRoleGroupMenus { get; set; }
        
    }
}