using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SuiviCompresseur.GestionResponsable.Domain.Models
{
    public class Filiale
    {
       
        public int FilialeID { get; set; }
       
        public string Nom { get; set; }
        public string Code { get; set; }
      
        
    

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }





    }
}
