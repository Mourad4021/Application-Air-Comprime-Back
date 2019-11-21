using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Models
{
    public class Fournisseur
    {
        [Key]
        public Guid FournisseurID { get; set; }
        [Required]
        // in the front: Nom=Fournisseur
        public string Nom { get; set; }
        public string Constructeur { get; set; }

        //[Range(1000, int.MaxValue, ErrorMessage = "veuillez entrer une valeur supérieure à {1000}")]
        public int Frequence_Des_Entretiens_Compresseur { get; set; }
        //[Range(1000, int.MaxValue, ErrorMessage = "Veuillez entrer une valeur superieure à {1000}")]
        public int Frequence_Des_Entretiens_Secheur { get; set; }
        public string Adresse { get; set; }
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-mail non valide")]

        public string Email { get; set; }
        public bool Active { get; set; }

        public ICollection<AttachementFournisseur> AttachementFournisseurs { get; set; }

    }
}
