﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuiviCompresseur.GestionFournisseur.Data.Context;

namespace SuiviCompresseur.GestionFournisseur.Data.Migrations
{
    [DbContext(typeof(FournisseurDbContext))]
    [Migration("20191115145600_456")]
    partial class _456
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SuiviCompresseur.GestionFournisseur.Domain.Models.Fournisseur", b =>
                {
                    b.Property<Guid>("FournisseurID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Adresse");

                    b.Property<string>("Constructeur");

                    b.Property<string>("Email");

                    b.Property<int>("Frequence_Des_Entretiens_Compresseur");

                    b.Property<int>("Frequence_Des_Entretiens_Secheur");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.HasKey("FournisseurID");

                    b.ToTable("Fournisseurs");
                });
#pragma warning restore 612, 618
        }
    }
}
