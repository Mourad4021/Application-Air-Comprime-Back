using Microsoft.AspNetCore.Http;
using SuiviCompresseur.GestionCompresseur.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.DTOs
{
    public class FicheSuiviDTOForPost
    {
      

        public Guid FicheSuiviID { set; get; }
        public DateTime Date { get; set; }
        public Guid EquipementFilialeID { get; set; }
        public int Nbre_Heurs_Total { set; get; }
        public int Nbre_Heurs_Charge { set; get; }
        public int Index_Electrique { set; get; }
        public double TempsArret { get; set; }
        public ListeEtat Etat { get; set; }
        public string FrequenceEentretienDeshuileur { get; set; }
        public double CourantAbsorbePhase { get; set; }
        public double FraisEntretienReparation { get; set; }
        public double PriseCompteur { get; set; }
        public double THuileC { get; set; }
        public string TSecheurC { get; set; }
        public string Remarques { get; set; }
        public ICollection<IFormFile> Attachments { get; set; }

    }
   
}
