using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Equip_Filiales_Comp_Sech
    {
        [Key]
        public Guid EquipementFilialeCompSechID { get; set; }

        public double PrixAcquisition { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAcquisition { get; set; }
        public int NumSerie { get; set; }

        //
        public bool HaveDebitMetre { get; set; }
        public bool HaveElectricCounter { get; set; }
        //
        public Guid EFID { get; set; }
        public EquipementFiliale EquipementFiliale { get; set; }
    }
}
