using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class GRH
    {
      
[Key]
       public Guid GRhID { get; set; }
      public Guid EquipementFilialeID { get; set; }
        public EquipementFiliale EquipementFiliale { get; set; }

       [Required]
       public decimal Salaire { get; set; }
 
       [Required]
       public float TauxAffectationAirComprime { get; set; }
      
       [Required]
       [DataType(DataType.Date)]
       public DateTime Date { get; set; }





    }

}
