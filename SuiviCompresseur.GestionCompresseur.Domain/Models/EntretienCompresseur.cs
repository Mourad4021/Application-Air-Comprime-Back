using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class EntretienCompresseur
    {
        [Key]
        public Guid EntretienCompresseurID { get; set; }
        public Guid EquipementFilialeID { get; set; }
        public int TypeEntretien { get; set; }
        public int PriseCompteurActuelle { get; set; }
        public int PriseCompteurDernierEntretien { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDernierEntretien { get; set; }
        public int ValeurCompteurProchainEntretien { get; set; }
        public string Commentaires { get; set; }
      //  public bool active { get; set; }
    }
}
