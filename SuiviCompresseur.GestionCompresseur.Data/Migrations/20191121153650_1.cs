using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipement",
                columns: table => new
                {
                    EquipementID = table.Column<Guid>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    FournisseurID = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Debit = table.Column<double>(nullable: true),
                    Puissance = table.Column<double>(nullable: true),
                    PuissanceCharge = table.Column<double>(nullable: true),
                    PuissanceVide = table.Column<double>(nullable: true),
                    Capacite = table.Column<double>(nullable: true),
                    PMS = table.Column<double>(nullable: true),
                    PE = table.Column<double>(nullable: true),
                    AnneeFabrication = table.Column<int>(nullable: true),
                    CapaciteTraitement = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipement", x => x.EquipementID);
                });

            migrationBuilder.CreateTable(
                name: "EquipementFiliale",
                columns: table => new
                {
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    EquipementID = table.Column<Guid>(nullable: false),
                    FilialeID = table.Column<Guid>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipementFiliale", x => x.EquipementFilialeID);
                    table.ForeignKey(
                        name: "FK_EquipementFiliale_Equipement_EquipementID",
                        column: x => x.EquipementID,
                        principalTable: "Equipement",
                        principalColumn: "EquipementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consommable",
                columns: table => new
                {
                    ConsommableID = table.Column<Guid>(nullable: false),
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    ConsommationComp = table.Column<int>(nullable: false),
                    PrixUnitaire = table.Column<decimal>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FraisElectriciteMensuel = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consommable", x => x.ConsommableID);
                    table.ForeignKey(
                        name: "FK_Consommable_EquipementFiliale_EquipementFilialeID",
                        column: x => x.EquipementFilialeID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntretienCompresseurs",
                columns: table => new
                {
                    EntretienCompresseurID = table.Column<Guid>(nullable: false),
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    TypeEntretien = table.Column<int>(nullable: false),
                    PriseCompteurActuelle = table.Column<int>(nullable: false),
                    PriseCompteurDernierEntretien = table.Column<int>(nullable: false),
                    DateDernierEntretien = table.Column<DateTime>(nullable: false),
                    ValeurCompteurProchainEntretien = table.Column<int>(nullable: false),
                    Commentaires = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntretienCompresseurs", x => x.EntretienCompresseurID);
                    table.ForeignKey(
                        name: "FK_EntretienCompresseurs_EquipementFiliale_EquipementFilialeID",
                        column: x => x.EquipementFilialeID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntretienReservoirs",
                columns: table => new
                {
                    EntretienReservoirID = table.Column<Guid>(nullable: false),
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    NatureVisite = table.Column<int>(nullable: false),
                    DerniereVisite = table.Column<DateTime>(nullable: false),
                    ProchaineVisite = table.Column<DateTime>(nullable: false),
                    Commentaires = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntretienReservoirs", x => x.EntretienReservoirID);
                    table.ForeignKey(
                        name: "FK_EntretienReservoirs_EquipementFiliale_EquipementFilialeID",
                        column: x => x.EquipementFilialeID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equip_Filiales_Comp_Sech",
                columns: table => new
                {
                    EquipementFilialeCompSechID = table.Column<Guid>(nullable: false),
                    PrixAcquisition = table.Column<double>(nullable: false),
                    DateAcquisition = table.Column<DateTime>(nullable: false),
                    NumSerie = table.Column<int>(nullable: false),
                    HaveDebitMetre = table.Column<bool>(nullable: false),
                    HaveElectricCounter = table.Column<bool>(nullable: false),
                    EFID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equip_Filiales_Comp_Sech", x => x.EquipementFilialeCompSechID);
                    table.ForeignKey(
                        name: "FK_Equip_Filiales_Comp_Sech_EquipementFiliale_EFID",
                        column: x => x.EFID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fiche_Suivis",
                columns: table => new
                {
                    FicheSuiviID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    Nbre_Heurs_Total = table.Column<int>(nullable: false),
                    Nbre_Heurs_Charge = table.Column<int>(nullable: false),
                    Index_Electrique = table.Column<int>(nullable: false),
                    TempsArret = table.Column<double>(nullable: false),
                    Etat = table.Column<int>(nullable: false),
                    Index_Debitmetre = table.Column<int>(nullable: false),
                    PointDeRoseeDuSecheur = table.Column<string>(nullable: true),
                    TypeDernierEntretien = table.Column<int>(nullable: false),
                    PriseCompteurDernierEntretien = table.Column<int>(nullable: false),
                    NombreHeuresProductionUsineLeJourPrecedent = table.Column<int>(nullable: false),
                    NombreDeJoursOuvrablesDuMois = table.Column<int>(nullable: false),
                    FraisEntretienReparation = table.Column<double>(nullable: false),
                    THuileC = table.Column<double>(nullable: false),
                    Remarques = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiche_Suivis", x => x.FicheSuiviID);
                    table.ForeignKey(
                        name: "FK_Fiche_Suivis_EquipementFiliale_EquipementFilialeID",
                        column: x => x.EquipementFilialeID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRH",
                columns: table => new
                {
                    GRhID = table.Column<Guid>(nullable: false),
                    EquipementFilialeID = table.Column<Guid>(nullable: false),
                    Salaire = table.Column<decimal>(nullable: false),
                    TauxAffectationAirComprime = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ChargesMensuelles = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRH", x => x.GRhID);
                    table.ForeignKey(
                        name: "FK_GRH_EquipementFiliale_EquipementFilialeID",
                        column: x => x.EquipementFilialeID,
                        principalTable: "EquipementFiliale",
                        principalColumn: "EquipementFilialeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(nullable: false),
                    AttachmentName = table.Column<string>(nullable: true),
                    AttachmentFileFormat = table.Column<string>(nullable: true),
                    AttachmentPhysicalPath = table.Column<string>(nullable: true),
                    AttachmentOriginFileName = table.Column<string>(nullable: true),
                    AttachementUploadDate = table.Column<DateTime>(nullable: false),
                    FicheSuiviID = table.Column<Guid>(nullable: true),
                    EntretienReservoirID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachments_EntretienReservoirs_EntretienReservoirID",
                        column: x => x.EntretienReservoirID,
                        principalTable: "EntretienReservoirs",
                        principalColumn: "EntretienReservoirID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Fiche_Suivis_FicheSuiviID",
                        column: x => x.FicheSuiviID,
                        principalTable: "Fiche_Suivis",
                        principalColumn: "FicheSuiviID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EntretienReservoirID",
                table: "Attachments",
                column: "EntretienReservoirID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_FicheSuiviID",
                table: "Attachments",
                column: "FicheSuiviID");

            migrationBuilder.CreateIndex(
                name: "IX_Consommable_EquipementFilialeID",
                table: "Consommable",
                column: "EquipementFilialeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntretienCompresseurs_EquipementFilialeID",
                table: "EntretienCompresseurs",
                column: "EquipementFilialeID");

            migrationBuilder.CreateIndex(
                name: "IX_EntretienReservoirs_EquipementFilialeID",
                table: "EntretienReservoirs",
                column: "EquipementFilialeID");

            migrationBuilder.CreateIndex(
                name: "IX_Equip_Filiales_Comp_Sech_EFID",
                table: "Equip_Filiales_Comp_Sech",
                column: "EFID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipementFiliale_EquipementID",
                table: "EquipementFiliale",
                column: "EquipementID");

            migrationBuilder.CreateIndex(
                name: "IX_Fiche_Suivis_EquipementFilialeID",
                table: "Fiche_Suivis",
                column: "EquipementFilialeID");

            migrationBuilder.CreateIndex(
                name: "IX_GRH_EquipementFilialeID",
                table: "GRH",
                column: "EquipementFilialeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Consommable");

            migrationBuilder.DropTable(
                name: "EntretienCompresseurs");

            migrationBuilder.DropTable(
                name: "Equip_Filiales_Comp_Sech");

            migrationBuilder.DropTable(
                name: "GRH");

            migrationBuilder.DropTable(
                name: "EntretienReservoirs");

            migrationBuilder.DropTable(
                name: "Fiche_Suivis");

            migrationBuilder.DropTable(
                name: "EquipementFiliale");

            migrationBuilder.DropTable(
                name: "Equipement");
        }
    }
}
