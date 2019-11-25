using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionFournisseur.Data.Migrations
{
    public partial class AttachFournisseu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    FournisseurID = table.Column<Guid>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    Constructeur = table.Column<string>(nullable: true),
                    Frequence_Des_Entretiens_Compresseur = table.Column<int>(nullable: false),
                    Frequence_Des_Entretiens_Secheur = table.Column<int>(nullable: false),
                    Adresse = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.FournisseurID);
                });

            migrationBuilder.CreateTable(
                name: "AttachementFournisseurs",
                columns: table => new
                {
                    AttachmentFournisseurId = table.Column<Guid>(nullable: false),
                    AttachmentNameF = table.Column<string>(nullable: true),
                    AttachmentFileFormatF = table.Column<string>(nullable: true),
                    AttachmentPhysicalPathF = table.Column<string>(nullable: true),
                    AttachmentOriginFileNameF = table.Column<string>(nullable: true),
                    AttachementUploadDateF = table.Column<DateTime>(nullable: false),
                    FournisseurID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachementFournisseurs", x => x.AttachmentFournisseurId);
                    table.ForeignKey(
                        name: "FK_AttachementFournisseurs_Fournisseurs_FournisseurID",
                        column: x => x.FournisseurID,
                        principalTable: "Fournisseurs",
                        principalColumn: "FournisseurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachementFournisseurs_FournisseurID",
                table: "AttachementFournisseurs",
                column: "FournisseurID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachementFournisseurs");

            migrationBuilder.DropTable(
                name: "Fournisseurs");
        }
    }
}
