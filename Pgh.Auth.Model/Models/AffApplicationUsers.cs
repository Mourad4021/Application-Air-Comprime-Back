using System;
using System.Collections.Generic;

namespace Pgh.Auth.Model.Models
{
    public class AffApplicationUsers
    {
        public Guid AppId { get; set; }
        public Guid UsersId { get; set; }
        public string Password { get; set; }
        
        public virtual Applications App { get; set; }
        public virtual Users Users { get; set; }

    }
}