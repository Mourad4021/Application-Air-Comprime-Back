﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuiviCompresseur.GestionCompresseur.Data.Context;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    [DbContext(typeof(CompresseurDbContext))]
    partial class CompresseurDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AttachementUploadDate");

                    b.Property<string>("AttachmentFileFormat");

                    b.Property<string>("AttachmentName");

                    b.Property<string>("AttachmentOriginFileName");

                    b.Property<string>("AttachmentPhysicalPath");

                    b.Property<Guid?>("EntretienReservoirID");

                    b.Property<Guid?>("FicheSuiviID");

                    b.HasKey("AttachmentId");

                    b.HasIndex("EntretienReservoirID");

                    b.HasIndex("FicheSuiviID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Consommable", b =>
                {
                    b.Property<Guid>("ConsommableID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConsommationComp");

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("EquipementFilialeID");

                    b.Property<decimal>("FraisElectriciteMensuel");

                    b.Property<decimal>("PrixUnitaire");

                    b.HasKey("ConsommableID");

                    b.HasIndex("EquipementFilialeID");

                    b.ToTable("Consommable");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EntretienCompresseur", b =>
                {
                    b.Property<Guid>("EntretienCompresseurID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Commentaires");

                    b.Property<DateTime>("DateDernierEntretien");

                    b.Property<Guid>("EquipementFilialeID");

                    b.Property<int>("PriseCompteurActuelle");

                    b.Property<int>("PriseCompteurDernierEntretien");

                    b.Property<int>("TypeEntretien");

                    b.Property<int>("ValeurCompteurProchainEntretien");

                    b.HasKey("EntretienCompresseurID");

                    b.HasIndex("EquipementFilialeID");

                    b.ToTable("EntretienCompresseurs");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EntretienReservoir", b =>
                {
                    b.Property<Guid>("EntretienReservoirID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Commentaires");

                    b.Property<DateTime>("DerniereVisite");

                    b.Property<Guid>("EquipementFilialeID");

                    b.Property<int>("NatureVisite");

                    b.Property<DateTime>("ProchaineVisite");

                    b.HasKey("EntretienReservoirID");

                    b.HasIndex("EquipementFilialeID");

                    b.ToTable("EntretienReservoirs");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Equip_Filiales_Comp_Sech", b =>
                {
                    b.Property<Guid>("EquipementFilialeCompSechID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAcquisition");

                    b.Property<Guid>("EFID");

                    b.Property<int>("NumSerie");

                    b.Property<double>("PrixAcquisition");

                    b.HasKey("EquipementFilialeCompSechID");

                    b.HasIndex("EFID")
                        .IsUnique();

                    b.ToTable("Equip_Filiales_Comp_Sech");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Equipement", b =>
                {
                    b.Property<Guid>("EquipementID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("FournisseurID");

                    b.Property<string>("Nom");

                    b.HasKey("EquipementID");

                    b.ToTable("Equipement");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Equipement");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", b =>
                {
                    b.Property<Guid>("EquipementFilialeID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("EquipementID");

                    b.Property<Guid>("FilialeID");

                    b.Property<string>("Nom");

                    b.HasKey("EquipementFilialeID");

                    b.HasIndex("EquipementID");

                    b.ToTable("EquipementFiliale");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Fiche_Suivi", b =>
                {
                    b.Property<Guid>("FicheSuiviID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CourantAbsorbePhase");

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("EquipementFilialeID");

                    b.Property<int>("Etat");

                    b.Property<double>("FraisEntretienReparation");

                    b.Property<string>("FrequenceEentretienDeshuileur");

                    b.Property<int>("Index_Electrique");

                    b.Property<int>("Nbre_Heurs_Charge");

                    b.Property<int>("Nbre_Heurs_Total");

                    b.Property<double>("PriseCompteur");

                    b.Property<string>("Remarques");

                    b.Property<double>("THuileC");

                    b.Property<string>("TSecheurC");

                    b.Property<double>("TempsArret");

                    b.HasKey("FicheSuiviID");

                    b.HasIndex("EquipementFilialeID");

                    b.ToTable("Fiche_Suivis");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.GRH", b =>
                {
                    b.Property<Guid>("GRhID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("EquipementFilialeID");

                    b.Property<decimal>("Salaire");

                    b.Property<float>("TauxAffectationAirComprime");

                    b.HasKey("GRhID");

                    b.HasIndex("EquipementFilialeID");

                    b.ToTable("GRH");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Compresseur", b =>
                {
                    b.HasBaseType("SuiviCompresseur.GestionCompresseur.Domain.Models.Equipement");

                    b.Property<double>("Debit");

                    b.Property<double>("Puissance");

                    b.Property<double>("PuissanceCharge");

                    b.Property<double>("PuissanceVide");

                    b.ToTable("Compresseur");

                    b.HasDiscriminator().HasValue("Compresseur");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Reservoir", b =>
                {
                    b.HasBaseType("SuiviCompresseur.GestionCompresseur.Domain.Models.Equipement");

                    b.Property<int>("AnneeFabrication");

                    b.Property<double>("Capacite");

                    b.Property<double>("PE");

                    b.Property<double>("PMS");

                    b.ToTable("Reservoir");

                    b.HasDiscriminator().HasValue("Reservoir");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Secheur", b =>
                {
                    b.HasBaseType("SuiviCompresseur.GestionCompresseur.Domain.Models.Equipement");

                    b.Property<double>("CapaciteTraitement");

                    b.ToTable("Secheur");

                    b.HasDiscriminator().HasValue("Secheur");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Attachment", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EntretienReservoir", "EntretienReservoir")
                        .WithMany("Attachments")
                        .HasForeignKey("EntretienReservoirID");

                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.Fiche_Suivi", "ficheSuivi")
                        .WithMany("Attachments")
                        .HasForeignKey("FicheSuiviID");
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Consommable", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", "EquipementFiliale")
                        .WithMany("Consommables")
                        .HasForeignKey("EquipementFilialeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EntretienCompresseur", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale")
                        .WithMany("EntretienCompresseurs")
                        .HasForeignKey("EquipementFilialeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EntretienReservoir", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", "EquipementFiliale")
                        .WithMany("EntretienReservoirs")
                        .HasForeignKey("EquipementFilialeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Equip_Filiales_Comp_Sech", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", "EquipementFiliale")
                        .WithOne("EFCompSech")
                        .HasForeignKey("SuiviCompresseur.GestionCompresseur.Domain.Models.Equip_Filiales_Comp_Sech", "EFID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.Equipement", "Equipement")
                        .WithMany("EquipementFiliales")
                        .HasForeignKey("EquipementID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.Fiche_Suivi", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", "EquipementFiliale")
                        .WithMany("Fiche_Suivis")
                        .HasForeignKey("EquipementFilialeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SuiviCompresseur.GestionCompresseur.Domain.Models.GRH", b =>
                {
                    b.HasOne("SuiviCompresseur.GestionCompresseur.Domain.Models.EquipementFiliale", "EquipementFiliale")
                        .WithMany("GRHs")
                        .HasForeignKey("EquipementFilialeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
