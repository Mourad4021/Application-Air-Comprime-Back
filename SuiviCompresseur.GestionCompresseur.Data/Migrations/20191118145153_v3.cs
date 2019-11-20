using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NombreDeJoursOuvrablesDuMois",
                table: "Fiche_Suivis",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreDeJoursOuvrablesDuMois",
                table: "Fiche_Suivis");
        }
    }
}
