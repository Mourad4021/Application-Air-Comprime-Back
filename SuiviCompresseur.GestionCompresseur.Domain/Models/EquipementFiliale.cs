using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class EquipementFiliale
    {
        [Key]
        public Guid EquipementFilialeID { get; set; }
        public Guid EquipementID { get; set; }
        public Guid FilialeID { get; set; }
        // Concatination du constructeur+typeComp+num
        public string Nom {get; set; }

        //  [RegularExpression("^[0-9]*[.]?[0-9]+", ErrorMessage = "UPRN must be numeric")] 
        //public float PrixAcquisition { get; set; }
       // [DataType(DataType.Date)]
       // public DateTime DateAcquisition { get; set; }
     //   public int NumSerie { get; set; }
        public bool Active { get; set; }

        public Equipement Equipement { get; set; }
        public virtual ICollection<Consommable>Consommables { get; set; }
        public virtual ICollection<Fiche_Suivi> Fiche_Suivis { get; set; }
        public virtual ICollection<GRH> GRHs { get; set; }
        public virtual ICollection<EntretienCompresseur> EntretienCompresseurs { get; set; }
        public virtual ICollection<EntretienReservoir> EntretienReservoirs { get; set; } 


        public Equip_Filiales_Comp_Sech EFCompSech { get; set; }





    }
}
