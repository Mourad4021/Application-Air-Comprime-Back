using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.GestionCompresseur.Data.Migrations
{
    public partial class frattachement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FournisseurID",
                table: "Attachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FournisseurID",
                table: "Attachments");
        }
    }
}
