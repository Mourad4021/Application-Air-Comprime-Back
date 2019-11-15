using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class _12345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_EntretienReservoirs_EntretienReservoirID",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Fiche_Suivis_FicheSuiviID",
                table: "Attachments");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_EntretienReservoirs_EntretienReservoirID",
                table: "Attachments",
                column: "EntretienReservoirID",
                principalTable: "EntretienReservoirs",
                principalColumn: "EntretienReservoirID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Fiche_Suivis_FicheSuiviID",
                table: "Attachments",
                column: "FicheSuiviID",
                principalTable: "Fiche_Suivis",
                principalColumn: "FicheSuiviID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_EntretienReservoirs_EntretienReservoirID",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Fiche_Suivis_FicheSuiviID",
                table: "Attachments");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_EntretienReservoirs_EntretienReservoirID",
                table: "Attachments",
                column: "EntretienReservoirID",
                principalTable: "EntretienReservoirs",
                principalColumn: "EntretienReservoirID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Fiche_Suivis_FicheSuiviID",
                table: "Attachments",
                column: "FicheSuiviID",
                principalTable: "Fiche_Suivis",
                principalColumn: "FicheSuiviID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
