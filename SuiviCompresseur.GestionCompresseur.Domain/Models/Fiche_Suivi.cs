using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using SuiviCompresseur.GestionCompresseur.Domain.Enum;
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
    
        //public double CourantAbsorbePhase { get; set; }
        
            
        //champ  ajouter le réunion de 15/11/2019
        public int Index_Debitmetre{ get; set; } //
        public string PointDeRoseeDuSecheur { get; set; } //
        public ListeTypeEntretien TypeDernierEntretien { get; set; }
        public int PriseCompteurDernierEntretien { get; set; }
        public int NombreHeuresProductionUsineLeJourPrecedent { get; set; }
        public int NombreDeJoursOuvrablesDuMois { get; set; }
        //
        public double FraisEntretienReparation { get; set; }
       
        [Required]
        public double THuileC { get; set; }
        public string TSecheurC { get; set; }
        public string Remarques { get; set; }
        public  ICollection<Attachment> Attachments { get; set; }
        

    }

}
