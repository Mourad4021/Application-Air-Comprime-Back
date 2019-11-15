using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SuiviCompresseur.GestionResponsable.Domain.Models
{
    public class Utilisateur
    {
        
        public int UtilisateurID { get; set; }
      
        public string Nom { get; set; }
      
        public string  Login { get; set; }
        
        public string MotDePasse { get; set; }


        public int FilialeID { get; set; }
        public Filiale Filiale { get; set; }





    }
}
