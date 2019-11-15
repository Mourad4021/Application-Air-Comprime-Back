

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionResponsable.Data.Context
{
    public class Gestion_Responsable_DBContext: DbContext
    {
        public Gestion_Responsable_DBContext(DbContextOptions<Gestion_Responsable_DBContext> options) : base(options)
        { }

        public DbSet<Filiale> Filiales{ get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
      



    }
}
