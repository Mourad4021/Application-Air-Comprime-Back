using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionResponsableAplication.Models
{
    public class FilialeEnv
    {
        
        public int FilialeID { get; set; }
       
        public string Nom { get; set; }
      
        public string Code { get; set; }
      
        
    

        public virtual ICollection<UtilisateurEnv> utilisateurEnv { get; set; }





    }
}
