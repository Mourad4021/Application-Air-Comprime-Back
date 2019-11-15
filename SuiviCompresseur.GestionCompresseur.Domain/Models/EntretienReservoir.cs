using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class EntretienReservoir
    {
        [Key]
        public Guid EntretienReservoirID { get; set; }
        public Guid EquipementFilialeID { get; set; }
        public EquipementFiliale EquipementFiliale { get; set; }
        public NatureVisite NatureVisite { get; set; }
        public DateTime DerniereVisite { get; set; }
        public DateTime ProchaineVisite { get; set; }
        public string Commentaires { get; set; }

        public ICollection<Attachment> Attachments { get; set; }


    }
    public enum NatureVisite
    {
        Interieure,
        Exterieure,
        Officielle
    }
}
