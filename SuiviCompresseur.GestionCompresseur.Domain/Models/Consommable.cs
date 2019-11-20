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
       
 
        public decimal PrixUnitaire { get; set; }
        public Boolean Active { get; set; }

        public DateTime Date { get; set; }
        public decimal FraisElectriciteMensuel { get; set; }
        
      
        
     
        
    
} }
