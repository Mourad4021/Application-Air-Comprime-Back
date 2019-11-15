using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionResponsableAplication.Models
{
    public class UtilisateurEnv
    {
        
        public int UtilisateurID { get; set; }
      
        public string Nom { get; set; }
      
        public string  Login { get; set; }
        
        public string MotDePasse { get; set; }


        public int FilialeID { get; set; }
        public FilialeEnv filialeEnv { get; set; }





    }
}
