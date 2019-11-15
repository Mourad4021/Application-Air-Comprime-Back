using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Consommable
    {
        [Key]
        public Guid ConsommableID { get; set; }
        public Guid EquipementFilialeID { get; set; }
        public EquipementFiliale EquipementFiliale { get; set; }
           
        public int ConsommationComp { get; set; }
        [Required]
 
        public decimal PrixUnitaire { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public decimal FraisElectriciteMensuel { get; set; }
        
      
        
     
        
    
} }
