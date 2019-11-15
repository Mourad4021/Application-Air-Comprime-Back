using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Fiche_Suivi
    {
    [Key]
        public Guid FicheSuiviID { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
       public Guid EquipementFilialeID { get; set; }
        public EquipementFiliale EquipementFiliale { get; set; }


        [Required]
        public int Nbre_Heurs_Total { set; get; }
        [Required]
        public int Nbre_Heurs_Charge { set; get; }
        [Required]
        public int Index_Electrique { set; get; }
        public double TempsArret { get; set; }
        public ListeEtat Etat { get; set; }
        public string FrequenceEentretienDeshuileur { get; set; }
        public double CourantAbsorbePhase { get; set; }

        public double FraisEntretienReparation { get; set; }
        public double PriseCompteur { get; set; }
        [Required]
        public double THuileC { get; set; }
        public string TSecheurC { get; set; }
        public string Remarques { get; set; }
        public  ICollection<Attachment> Attachments { get; set; }
        

    }

}
