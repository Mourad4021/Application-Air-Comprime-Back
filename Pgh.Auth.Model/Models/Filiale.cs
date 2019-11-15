using System;
using System.Collections.Generic;
using System.Text;

namespace Pgh.Auth.Model.Models
{
    public   class Filiale
    {
        public Guid FilialeID { get; set; }

        public string Nom { get; set; }
        public string Code { get; set; }
        public string Conformite_d_Exploitation {get;set;}
        public bool Active { get; set; }




        public virtual ICollection<Users> Users { get; set; }



        
    }
}
