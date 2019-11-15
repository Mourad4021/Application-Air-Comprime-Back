using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public abstract class Equipement
    {
        [Key]
        public Guid EquipementID { get; set; }
        public string Nom { get; set; }
        public bool Active { get; set; }

        public Guid FournisseurID { get; set; }
        public virtual ICollection<EquipementFiliale> EquipementFiliales { get; set; }

    }
}
