using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Reservoir : Equipement
    {
        public double Capacite { get; set; }
        public double PMS { get; set; }
        public double PE { get; set; }

       // public DateTime AnneeFabrication { get; set; }

        public int AnneeFabrication { get; set; }
    }
    
}
