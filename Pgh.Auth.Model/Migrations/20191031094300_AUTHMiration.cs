using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pgh.Auth.Model.Migrations
{
    public partial class AUTHMiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    AppId = table.Column<Guid>(nullable: false),
                    AppCode = table.Column<string>(nullable: true),
                    AppName = table.Column<string>(nullable: true),
                    AppDisplayName = table.Column<string>(nullable: true),
                    AppDescription = table.Column<string>(nullable: true),
                    AppState = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "Filiale",
                columns: table => new
                {
                    FilialeID = table.Column<Guid>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Conformite_d_Exploitation = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiale", x => x.FilialeID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermId = table.Column<Guid>(nullable: false),
                    PermName = table.Column<string>(nullable: true),
                    PermDisplayName = table.Column<string>(nullable: true),
                    PermDescription = table.Column<string>(nullable: true),
                    PermState = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    RoleDisplayName = table.Column<string>(nullable: true),
                    RoleDescription = table.Column<string>(nullable: true),
                    RoleState = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Groupes",
                columns: table => new
                {
                    GrpId = table.Column<Guid>(nullable: false),
                    GrpName = table.Column<string>(nullable: true),
                    GrpDisplayName = table.Column<string>(nullable: true),
                    GrpDescription = table.Column<string>(nullable: true),
                    GrpState = table.Column<bool>(nullable: false),
                    FkAppId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupes", x => x.GrpId);
                    table.ForeignKey(
                        name: "FK_Groupes_Applications",
                        column: x => x.FkAppId,
                        principalTable: "Applications",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(nullable: false),
                    MenuName = table.Column<string>(nullable: true),
                    MenuDisplayName = table.Column<string>(nullable: true),
                    MenuDescription = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    MenuState = table.Column<bool>(nullable: false),
                    FkMenuId = table.Column<Guid>(nullable: true),
                    FkAppId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menus_Applications_FkAppId",
                        column: x => x.FkAppId,
                        principalTable: "Applications",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_FkMenuId",
                        column: x => x.FkMenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(nullable: false),
                    UsersCode = table.Column<string>(maxLength: 8, nullable: true),
                    UsersName = table.Column<string>(maxLength: 50, nullable: true),
                    UsersLastName = table.Column<string>(maxLength: 50, nullable: true),
                    UsersState = table.Column<bool>(nullable: false),
                    UsersMail = table.Column<string>(maxLength: 80, nullable: true),
                    UsersMailIntern = table.Column<string>(maxLength: 80, nullable: true),
                    UsersPosteName = table.Column<string>(nullable: true),
                    UsersPhoneNumber = table.Column<string>(nullable: true),
                    UsersPersonalNumber = table.Column<string>(nullable: true),
                    UsersGenderCode = table.Column<string>(nullable: true),
                    UsersBirthDate = table.Column<DateTime>(nullable: false),
                    UsersJoinDate = table.Column<DateTime>(nullable: false),
                    UsersDateLeave = table.Column<DateTime>(nullable: false),
                    FkUsersId = table.Column<Guid>(nullable: true),
                    FilialeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UsersId);
                    table.ForeignKey(
                        name: "FK_User_Filiale_FilialeID",
                        column: x => x.FilialeID,
                        principalTable: "Filiale",
                        principalColumn: "FilialeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_FkUsersId",
                        column: x => x.FkUsersId,
                        principalTable: "User",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AffRolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    PermId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffRolePermissions", x => new { x.PermId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AffRolePermissions_Permissions_PermId",
                        column: x => x.PermId,
                        principalTable: "Permissions",
                        principalColumn: "PermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffRolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffRoleGroupMenus",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    GrpId = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffRoleGroupMenus", x => new { x.GrpId, x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AffRoleGroupMenus_Groupes_GrpId",
                        column: x => x.GrpId,
                        principalTable: "Groupes",
                        principalColumn: "GrpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffRoleGroupMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffRoleGroupMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffApplicationUsers",
                columns: table => new
                {
                    AppId = table.Column<Guid>(nullable: false),
                    UsersId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffApplicationUsers", x => new { x.AppId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AffApplicationUsers_Applications_AppId",
                        column: x => x.AppId,
                        principalTable: "Applications",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffApplicationUsers_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffGroupUsers",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(nullable: false),
                    GrpId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffGroupUsers", x => new { x.GrpId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AffGroupUsers_Groupes_GrpId",
                        column: x => x.GrpId,
                        principalTable: "Groupes",
                        principalColumn: "GrpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffGroupUsers_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffRolesUsersMenus",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    UsersId = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffRolesUsersMenus", x => new { x.UsersId, x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AffRolesUsersMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffRolesUsersMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffRolesUsersMenus_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "UsersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "AppId", "AppCode", "AppDescription", "AppDisplayName", "AppName", "AppState" },
                values: new object[,]
                {
                    { new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), "0000", "Cette application gérer l'authentification et des permissions des différents applications.", "Gestion de l'authentification  des applications", "AuthApp", true },
                    { new Guid("006097eb-0fa8-46a3-9e21-fd5c1c61a22b"), "0001", "Cette application gérer le processus d'analyse des échantillons par le labo Dick.", "Gestion de laboratoire Dick", "LaboDickAgro", true },
                    { new Guid("5d5c79a2-e823-4a40-be1b-ecb54aa5f0a8"), "0013", "Mise a jour de l'ancienne application Laboratoir Dick.", "Gestion de laboratoire Dick Elevage", "LaboDickElevage", true }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermId", "PermDescription", "PermDisplayName", "PermName", "PermState" },
                values: new object[,]
                {
                    { new Guid("386c1a06-5e41-4687-90ae-3c9fc745061b"), "Users Will Have Permission to Manage personal views.", "Manage personal views", "ManagePersonalViews", true },
                    { new Guid("fa548165-aa7e-4893-a62e-d229637dcc96"), "Users Will Have Permission to view pages.", "View Pages", "ViewPages", true },
                    { new Guid("f0943f92-ce16-4b33-9726-d1188373ba3f"), "Users Will Have Permission to create groups.", "Create Groups", "CreateGroups", true },
                    { new Guid("371a6c85-c6e0-4107-85fb-8d82346911ee"), "Users Will Have Permission to view application pages.", "View application pages", "ViewApplicationPages", true },
                    { new Guid("1243ca02-d628-462e-8749-d84edb2afdc0"), "Users Will Have Permission to Delete versions.", "Delete Versions", "DeleteVersions", true },
                    { new Guid("a2d317d2-22e1-408a-8462-f42c985f0212"), "Users Will Have Permission to edit users personal information.", "Edit user's personal information", "EditUserPersonalInformation", true },
                    { new Guid("d69dc2b1-fe23-492b-a9f7-f0f8cd919663"), "Users Will Have Permission to approve items.", "Approve Items", "ApproveItems", true },
                    { new Guid("0972105b-0624-4b70-8134-106927670252"), "Users Will Have Permission to View Items.", "View Items", "ViewItems", true },
                    { new Guid("66eac8ff-df76-4864-85af-1e45777f88fc"), "Users Will Have Permission to delete Elements.", "Delete Items", "DeleteItems", true },
                    { new Guid("5ac089b5-2c81-496c-bfa7-64b440d5209f"), "Users Will Have Permission to edit Items.", "Edit Items", "EditItems", true },
                    { new Guid("5b30a049-0ed6-4e65-a571-db958ba2979b"), "Users Will Have the permission to add items", "Add Items", "AddItems", true },
                    { new Guid("c8ce40d9-ab3a-43b5-96cf-e337799b6749"), "Users Will Have Permission to show versions.", "Show Versions", "ShowVersions", true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleDescription", "RoleDisplayName", "RoleName", "RoleState" },
                values: new object[,]
                {
                    { new Guid("68d62329-3713-4ef4-b6f4-d2c2355aeca1"), "Restricted reading Default Groupe", "Restricted reading Groupe", "Restricted reading", true },
                    { new Guid("efebdd55-5a1b-492e-b736-9062340a9fbc"), "Approval Default Groupe", "Approval Groupe", "Approval", true },
                    { new Guid("aaa6e1a0-8534-4ec2-b9b3-f09df832df30"), "Display Only Default Groupe", "Display Only Groupe", "DisplayOnly", true },
                    { new Guid("4e4f22c1-fd30-469b-ac5d-b22725619cac"), "Limited Access Default Groupe", "Limited Access", "LimitedAccess", true },
                    { new Guid("224ef755-1794-4629-bdd2-fcb1d9ea00bc"), "Collaboration Default Groupe", "Collaboration Groupe", "Collaboration", true },
                    { new Guid("eef95016-fae3-4832-ad95-0011407d0944"), "Editors Groupe Default Groupe", "Editors Groupe", "Editors", true },
                    { new Guid("e2418633-f452-4877-807c-fbc1bd58de6c"), "Design Groupe Default Groupe", "Design Groupe", "Design", true },
                    { new Guid("c8e8463f-f460-4ae9-a8fb-8eaaabc81b1b"), "Total Control Default Groupe", "Total Control", "TotalControl", true },
                    { new Guid("4d6d732f-909d-4ebe-b98a-71f6dc074d6e"), "Readers Groupe Default Groupe", "Readers Groupe", "Readers", true }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UsersId", "FilialeID", "FkUsersId", "UsersBirthDate", "UsersCode", "UsersDateLeave", "UsersGenderCode", "UsersJoinDate", "UsersLastName", "UsersMail", "UsersMailIntern", "UsersName", "UsersPersonalNumber", "UsersPhoneNumber", "UsersPosteName", "UsersState" },
                values: new object[,]
                {
                    { new Guid("99dfeff6-d3c4-4199-bc4b-b5e23bc23d11"), null, null, new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2522), "00000002", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2523), "M", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2524), "Test", "User1@poulina.com", "User1@poulina.com", "User1", "63524163", "63524141", "User1 Poste", false },
                    { new Guid("9a55047d-13cc-4c6b-82eb-6d4e740a4f9f"), null, null, new DateTime(2019, 10, 31, 10, 43, 0, 306, DateTimeKind.Local).AddTicks(9671), "00000000", new DateTime(2019, 10, 31, 10, 43, 0, 307, DateTimeKind.Local).AddTicks(7133), "M", new DateTime(2019, 10, 31, 10, 43, 0, 307, DateTimeKind.Local).AddTicks(7750), "Admin", "Admin@poulina.com", "Admin@poulina.com", "Admin", "63524163", "63524141", "Admin Poste", false },
                    { new Guid("ca10e515-0486-487b-84f2-46571ace4542"), null, null, new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2479), "00000001", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2486), "M", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2491), "SupAdmin", "SupAdmin@poulina.com", "SupAdmin@poulina.com", "SupAdmin", "63524163", "63524141", "SupAdmin Poste", false },
                    { new Guid("2010c4f6-fa7d-422c-ba73-fc47009f4f8c"), null, null, new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2527), "00000003", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2528), "M", new DateTime(2019, 10, 31, 10, 43, 0, 308, DateTimeKind.Local).AddTicks(2529), "Test", "User2@poulina.com", "User2@poulina.com", "User2", "63524163", "63524141", "User2 Poste", false }
                });

            migrationBuilder.InsertData(
                table: "Groupes",
                columns: new[] { "GrpId", "FkAppId", "GrpDescription", "GrpDisplayName", "GrpName", "GrpState" },
                values: new object[,]
                {
                    { new Guid("04b68613-032c-439b-9b4f-e803eb6a7f4e"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), "Administration Groupe for Authentication Application", "Administration Groupes", "AdministratorAuth", true },
                    { new Guid("a5007005-07a5-4c03-9c18-73f318dda7e4"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), "Readers Groupe for Authentication Application", "Readers Groupes", "ReadersAuth", true },
                    { new Guid("fffdbf85-6e87-4de3-8790-4fba3a028e55"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), "Editors Groupe for Authentication Application", "Editors Groupes", "EditorsAuth", true }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "FkAppId", "FkMenuId", "MenuDescription", "MenuDisplayName", "MenuName", "MenuState", "MenuUrl" },
                values: new object[,]
                {
                    { new Guid("0c568f77-29e0-4a76-9278-13247f439a9e"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "User setup menu.", "Users", "AuthUsers", true, "Http://srvapp/authusers" },
                    { new Guid("45228d92-9b57-49c6-b1e1-6273f910937c"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "Permissions setup menu.", "Permissions", "AuthPermissions", true, "Http://srvapp/authpermissions" },
                    { new Guid("54348b0f-3f5a-4ece-b1fb-d960715ad129"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "Roles setup menu.", "Roles", "AuthRoles", true, "Http://srvapp/authroles" },
                    { new Guid("81064b62-7721-42aa-8bf0-67117bd0ca0f"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "Applications setup menu.", "Applications", "AuthApplications", true, "Http://srvapp/authappications" },
                    { new Guid("675314af-212c-4cc7-9f7f-1f91c3e3369a"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "Menus setup menu.", "Menus", "AuthMenus", true, "Http://srvapp/authmenus" },
                    { new Guid("e5f2b4f0-c976-47d3-97fe-870964ff3531"), new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"), null, "Groupes setup menu.", "Groupes", "AuthGroupes", true, "Http://srvapp/authgroupes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffApplicationUsers_UsersId",
                table: "AffApplicationUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AffGroupUsers_UsersId",
                table: "AffGroupUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AffRoleGroupMenus_MenuId",
                table: "AffRoleGroupMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AffRoleGroupMenus_RoleId",
                table: "AffRoleGroupMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AffRolePermissions_RoleId",
                table: "AffRolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AffRolesUsersMenus_MenuId",
                table: "AffRolesUsersMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AffRolesUsersMenus_RoleId",
                table: "AffRolesUsersMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppCode",
                table: "Applications",
                column: "AppCode",
                unique: true,
                filter: "([AppCode] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Groupes_FkAppId",
                table: "Groupes",
                column: "FkAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FkAppId",
                table: "Menus",
                column: "FkAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FkMenuId",
                table: "Menus",
                column: "FkMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermName",
                table: "Permissions",
                column: "PermName",
                unique: true,
                filter: "([PermName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true,
                filter: "([RoleName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_User_FilialeID",
                table: "User",
                column: "FilialeID");

            migrationBuilder.CreateIndex(
                name: "IX_User_FkUsersId",
                table: "User",
                column: "FkUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UsersCode",
                table: "User",
                column: "UsersCode",
                unique: true,
                filter: "([UsersCode] IS NOT NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffApplicationUsers");

            migrationBuilder.DropTable(
                name: "AffGroupUsers");

            migrationBuilder.DropTable(
                name: "AffRoleGroupMenus");

            migrationBuilder.DropTable(
                name: "AffRolePermissions");

            migrationBuilder.DropTable(
                name: "AffRolesUsersMenus");

            migrationBuilder.DropTable(
                name: "Groupes");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Filiale");
        }
    }
}
