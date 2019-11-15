using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Compresseur : Equipement
    {
        public double Debit { get; set; }
        public double Puissance { get; set; }
        public double PuissanceCharge { get; set; }
        public double PuissanceVide { get; set; }
      //  public double FrequenceEntretiensCompresseur { get; set; }
    }
}
