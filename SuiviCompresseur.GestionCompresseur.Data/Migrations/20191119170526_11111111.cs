using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class _11111111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ChargesMensuelles",
                table: "GRH",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargesMensuelles",
                table: "GRH");
        }
    }
}
