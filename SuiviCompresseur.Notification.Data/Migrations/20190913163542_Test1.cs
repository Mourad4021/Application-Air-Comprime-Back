using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuiviCompresseur.Notification.Data.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailFroms",
                columns: table => new
                {
                    IdMail = table.Column<Guid>(nullable: false),
                    FromName = table.Column<string>(nullable: true),
                    FromAddresses = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    MessageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailFroms", x => x.IdMail);
                });

            migrationBuilder.CreateTable(
                name: "EmailTos",
                columns: table => new
                {
                    IdTo = table.Column<Guid>(nullable: false),
                    IdMail = table.Column<Guid>(nullable: false),
                    ToName = table.Column<string>(nullable: true),
                    ToAddresses = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: false),
                    ReceiveType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTos", x => x.IdTo);
                    table.ForeignKey(
                        name: "FK_EmailTos_EmailFroms_IdMail",
                        column: x => x.IdMail,
                        principalTable: "EmailFroms",
                        principalColumn: "IdMail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTos_IdMail",
                table: "EmailTos",
                column: "IdMail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTos");

            migrationBuilder.DropTable(
                name: "EmailFroms");
        }
    }
}
