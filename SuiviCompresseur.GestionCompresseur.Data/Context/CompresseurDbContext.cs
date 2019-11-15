using SuiviCompresseur.GestionCompresseur.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Data.Context
{
    public class CompresseurDbContext : DbContext
    {
        public CompresseurDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EquipementFiliale> EquipementFiliales { get; set; }
        public DbSet<Consommable> Consommables { get; set; }
        public DbSet<Fiche_Suivi> Fiche_Suivis { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<Compresseur> Compresseurs { get; set; }
        public DbSet<Secheur> Secheurs { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<GRH> GRHs { get; set; }
        public DbSet<EntretienReservoir> EntretienReservoirs { get; set; }
        public DbSet<EntretienCompresseur> EntretienCompresseurs { get; set; }
        public DbSet<Equip_Filiales_Comp_Sech> EquipFilialesCompSeches { get; set; }

       
           


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compresseur>().ToTable("Compresseur");
            modelBuilder.Entity<Secheur>().ToTable("Secheur");
            modelBuilder.Entity<Reservoir>().ToTable("Reservoir");
            modelBuilder.Entity<EquipementFiliale>().ToTable("EquipementFiliale");
            modelBuilder.Entity<EquipementFiliale>()
                .HasOne(p => p.EFCompSech)
                .WithOne(b => b.EquipementFiliale)
                .HasForeignKey<Equip_Filiales_Comp_Sech>(x => x.EFID);
            modelBuilder.Entity<Consommable>().ToTable("Consommable");
            //one to many 
            modelBuilder.Entity<Fiche_Suivi>()
                .HasMany(c => c.Attachments)
                .WithOne(e => e.ficheSuivi)
.IsRequired(false);

            modelBuilder.Entity<EntretienReservoir>()
               .HasMany(c => c.Attachments)
               .WithOne(e => e.EntretienReservoir)
               .IsRequired(false);
            




            modelBuilder.Entity<GRH>().ToTable("GRH");
            modelBuilder.Entity<Equipement>().ToTable("Equipement");

            modelBuilder.Entity<Equip_Filiales_Comp_Sech>().ToTable("Equip_Filiales_Comp_Sech");

        }
    }
 
}
