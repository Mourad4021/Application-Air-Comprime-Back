using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Application.Models
{
    public class FournisseurCreation
    {
      
        public Guid FournisseurID { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
    }

}
