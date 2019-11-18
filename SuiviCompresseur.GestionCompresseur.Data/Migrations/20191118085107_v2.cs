using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourantAbsorbePhase",
                table: "Fiche_Suivis");

            migrationBuilder.DropColumn(
                name: "PriseCompteur",
                table: "Fiche_Suivis");

            migrationBuilder.RenameColumn(
                name: "FrequenceEentretienDeshuileur",
                table: "Fiche_Suivis",
                newName: "PointDeRoseeDuSecheur");

            migrationBuilder.AddColumn<int>(
                name: "Index_Debitmetre",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NombreHeuresProductionUsineLeJourPrecedent",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriseCompteurDernierEntretien",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeDernierEntretien",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HaveDebitMetre",
                table: "Equip_Filiales_Comp_Sech",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HaveElectricCounter",
                table: "Equip_Filiales_Comp_Sech",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index_Debitmetre",
                table: "Fiche_Suivis");

            migrationBuilder.DropColumn(
                name: "NombreHeuresProductionUsineLeJourPrecedent",
                table: "Fiche_Suivis");

            migrationBuilder.DropColumn(
                name: "PriseCompteurDernierEntretien",
                table: "Fiche_Suivis");

            migrationBuilder.DropColumn(
                name: "TypeDernierEntretien",
                table: "Fiche_Suivis");

            migrationBuilder.DropColumn(
                name: "HaveDebitMetre",
                table: "Equip_Filiales_Comp_Sech");

            migrationBuilder.DropColumn(
                name: "HaveElectricCounter",
                table: "Equip_Filiales_Comp_Sech");

            migrationBuilder.RenameColumn(
                name: "PointDeRoseeDuSecheur",
                table: "Fiche_Suivis",
                newName: "FrequenceEentretienDeshuileur");

            migrationBuilder.AddColumn<double>(
                name: "CourantAbsorbePhase",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriseCompteur",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
