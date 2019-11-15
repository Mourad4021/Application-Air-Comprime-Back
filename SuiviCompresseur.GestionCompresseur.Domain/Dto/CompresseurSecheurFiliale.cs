using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Dto
{
    public class CompresseurSecheurFiliale
    {
        public Guid EquipementFilialeID { get; set; }
        public Guid EquipementID { get; set; }
        public Guid FilialeID { get; set; }
        public string Nom { get; set; }
        public bool Active { get; set; }

        public Guid EquipementFilialeCompSechID { get; set; }
        public double PrixAcquisition { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAcquisition { get; set; }
        public int NumSerie { get; set; }
        public Guid EFID { get; set; }
    }
}
