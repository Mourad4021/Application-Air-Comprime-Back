
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Data.Context
{
    public class FournisseurDbContext : DbContext
    {
        public FournisseurDbContext(DbContextOptions<FournisseurDbContext> options) : base(options)
        {
        }

        public DbSet<Fournisseur> Fournisseurs { get; set; }
    }
}
