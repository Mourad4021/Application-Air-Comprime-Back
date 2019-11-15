using System;
using System.Collections.Generic;

namespace Pgh.Auth.Model.Models
{
    public class AffGroupUsers
    {
        public Guid UsersId { get; set; }
        public Guid GrpId { get; set; }
       


        public virtual Users Users { get; set; }
        public virtual Groupes Grp { get; set; }

    }
}