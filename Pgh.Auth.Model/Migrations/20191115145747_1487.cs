using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pgh.Auth.Model.Migrations
{
    public partial class _1487 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("006097eb-0fa8-46a3-9e21-fd5c1c61a22b"));

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("5d5c79a2-e823-4a40-be1b-ecb54aa5f0a8"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("04b68613-032c-439b-9b4f-e803eb6a7f4e"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("a5007005-07a5-4c03-9c18-73f318dda7e4"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("fffdbf85-6e87-4de3-8790-4fba3a028e55"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("0c568f77-29e0-4a76-9278-13247f439a9e"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("45228d92-9b57-49c6-b1e1-6273f910937c"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("54348b0f-3f5a-4ece-b1fb-d960715ad129"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("675314af-212c-4cc7-9f7f-1f91c3e3369a"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("81064b62-7721-42aa-8bf0-67117bd0ca0f"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("e5f2b4f0-c976-47d3-97fe-870964ff3531"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("0972105b-0624-4b70-8134-106927670252"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("1243ca02-d628-462e-8749-d84edb2afdc0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("371a6c85-c6e0-4107-85fb-8d82346911ee"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("386c1a06-5e41-4687-90ae-3c9fc745061b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("5ac089b5-2c81-496c-bfa7-64b440d5209f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("5b30a049-0ed6-4e65-a571-db958ba2979b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("66eac8ff-df76-4864-85af-1e45777f88fc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("a2d317d2-22e1-408a-8462-f42c985f0212"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("c8ce40d9-ab3a-43b5-96cf-e337799b6749"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("d69dc2b1-fe23-492b-a9f7-f0f8cd919663"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("f0943f92-ce16-4b33-9726-d1188373ba3f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("fa548165-aa7e-4893-a62e-d229637dcc96"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("224ef755-1794-4629-bdd2-fcb1d9ea00bc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("4d6d732f-909d-4ebe-b98a-71f6dc074d6e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("4e4f22c1-fd30-469b-ac5d-b22725619cac"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("68d62329-3713-4ef4-b6f4-d2c2355aeca1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("aaa6e1a0-8534-4ec2-b9b3-f09df832df30"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c8e8463f-f460-4ae9-a8fb-8eaaabc81b1b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("e2418633-f452-4877-807c-fbc1bd58de6c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("eef95016-fae3-4832-ad95-0011407d0944"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("efebdd55-5a1b-492e-b736-9062340a9fbc"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("2010c4f6-fa7d-422c-ba73-fc47009f4f8c"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("99dfeff6-d3c4-4199-bc4b-b5e23bc23d11"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("9a55047d-13cc-4c6b-82eb-6d4e740a4f9f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("ca10e515-0486-487b-84f2-46571ace4542"));

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("7d84faba-eb47-4861-bfa3-816c9d341a15"));

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "AppId", "AppCode", "AppDescription", "AppDisplayName", "AppName", "AppState" },
                values: new object[,]
                {
                    { new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), "0000", "Cette application gérer l'authentification et des permissions des différents applications.", "Gestion de l'authentification  des applications", "AuthApp", true },
                    { new Guid("5bf9211a-df6c-4f3f-a059-652fc2194f33"), "0001", "Cette application gérer le processus d'analyse des échantillons par le labo Dick.", "Gestion de laboratoire Dick", "LaboDickAgro", true },
                    { new Guid("b1335dae-e24d-4066-b8ee-246cbdfbb634"), "0013", "Mise a jour de l'ancienne application Laboratoir Dick.", "Gestion de laboratoire Dick Elevage", "LaboDickElevage", true }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermId", "PermDescription", "PermDisplayName", "PermName", "PermState" },
                values: new object[,]
                {
                    { new Guid("b1bc9f48-a943-4564-822c-dc070b2fb2b9"), "Users Will Have Permission to Manage personal views.", "Manage personal views", "ManagePersonalViews", true },
                    { new Guid("32b7cfae-4130-4d07-962a-783ec5658307"), "Users Will Have Permission to view pages.", "View Pages", "ViewPages", true },
                    { new Guid("90d0882d-1646-43c6-b243-862e0078656e"), "Users Will Have Permission to create groups.", "Create Groups", "CreateGroups", true },
                    { new Guid("5a3583d9-034b-48e2-8815-717114cf657e"), "Users Will Have Permission to view application pages.", "View application pages", "ViewApplicationPages", true },
                    { new Guid("c7fb5a2e-a6f3-4779-9520-59470db130a6"), "Users Will Have Permission to Delete versions.", "Delete Versions", "DeleteVersions", true },
                    { new Guid("ac65f7fc-f494-477f-9946-92ef63bc3fee"), "Users Will Have Permission to edit users personal information.", "Edit user's personal information", "EditUserPersonalInformation", true },
                    { new Guid("a8cd99c7-b339-485e-a747-be4f4472fdb7"), "Users Will Have Permission to approve items.", "Approve Items", "ApproveItems", true },
                    { new Guid("1fc80226-5dd8-4dc1-b1fd-602376cf27ae"), "Users Will Have Permission to View Items.", "View Items", "ViewItems", true },
                    { new Guid("51bd26d3-2765-4d4d-bb12-323081faf3ed"), "Users Will Have Permission to delete Elements.", "Delete Items", "DeleteItems", true },
                    { new Guid("0b6add34-d44a-4197-9553-7950fddaf8c1"), "Users Will Have Permission to edit Items.", "Edit Items", "EditItems", true },
                    { new Guid("d9bbc3ef-0cd0-4688-aee7-28ea203c7602"), "Users Will Have the permission to add items", "Add Items", "AddItems", true },
                    { new Guid("de8f86f3-a5ba-4295-9378-2561193d7c79"), "Users Will Have Permission to show versions.", "Show Versions", "ShowVersions", true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleDescription", "RoleDisplayName", "RoleName", "RoleState" },
                values: new object[,]
                {
                    { new Guid("b7492c1c-5268-43fd-bcee-c6227c0d863a"), "Restricted reading Default Groupe", "Restricted reading Groupe", "Restricted reading", true },
                    { new Guid("2dbf8533-9b2f-4443-a5b9-d66e435fcc1e"), "Approval Default Groupe", "Approval Groupe", "Approval", true },
                    { new Guid("f676ec19-cdb6-4d6c-9415-d8f51e75438f"), "Display Only Default Groupe", "Display Only Groupe", "DisplayOnly", true },
                    { new Guid("e41b1a8a-7a20-4799-aaee-b836dd28c4ae"), "Limited Access Default Groupe", "Limited Access", "LimitedAccess", true },
                    { new Guid("79839a7a-8ad5-40f3-8d40-e313b7c836a4"), "Collaboration Default Groupe", "Collaboration Groupe", "Collaboration", true },
                    { new Guid("fa2cc2dd-547a-418d-8273-cd3a0d30af0b"), "Editors Groupe Default Groupe", "Editors Groupe", "Editors", true },
                    { new Guid("02420da9-1db8-425b-ba7e-cb88710ad90b"), "Design Groupe Default Groupe", "Design Groupe", "Design", true },
                    { new Guid("f722bd9a-03c2-45f9-be59-6106b92717e0"), "Total Control Default Groupe", "Total Control", "TotalControl", true },
                    { new Guid("c5f5ab50-a938-486a-88b5-bf97bef4bdfa"), "Readers Groupe Default Groupe", "Readers Groupe", "Readers", true }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UsersId", "FilialeID", "FkUsersId", "UsersBirthDate", "UsersCode", "UsersDateLeave", "UsersGenderCode", "UsersJoinDate", "UsersLastName", "UsersMail", "UsersMailIntern", "UsersName", "UsersPersonalNumber", "UsersPhoneNumber", "UsersPosteName", "UsersState" },
                values: new object[,]
                {
                    { new Guid("e950be8b-64f9-4b63-a12d-da9e417a7ca8"), null, null, new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3428), "00000002", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3428), "M", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3429), "Test", "User1@poulina.com", "User1@poulina.com", "User1", "63524163", "63524141", "User1 Poste", false },
                    { new Guid("26009217-051a-4a16-962d-2e88c311c086"), null, null, new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(781), "00000000", new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(9376), "M", new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(9849), "Admin", "Admin@poulina.com", "Admin@poulina.com", "Admin", "63524163", "63524141", "Admin Poste", false },
                    { new Guid("1edc2e9f-c013-4e95-b319-b5647e086be5"), null, null, new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3396), "00000001", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3400), "M", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3404), "SupAdmin", "SupAdmin@poulina.com", "SupAdmin@poulina.com", "SupAdmin", "63524163", "63524141", "SupAdmin Poste", false },
                    { new Guid("fe14a91b-8b81-47ee-86bc-c73802330da7"), null, null, new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3432), "00000003", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3432), "M", new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3433), "Test", "User2@poulina.com", "User2@poulina.com", "User2", "63524163", "63524141", "User2 Poste", false }
                });

            migrationBuilder.InsertData(
                table: "Groupes",
                columns: new[] { "GrpId", "FkAppId", "GrpDescription", "GrpDisplayName", "GrpName", "GrpState" },
                values: new object[,]
                {
                    { new Guid("c0f011a4-dd60-4599-99d4-464b1272fe99"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), "Administration Groupe for Authentication Application", "Administration Groupes", "AdministratorAuth", true },
                    { new Guid("70744253-0bd2-4a26-b549-be8568df9bdd"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), "Readers Groupe for Authentication Application", "Readers Groupes", "ReadersAuth", true },
                    { new Guid("e3459c31-dbff-4bc7-b7d8-974fb16d18f4"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), "Editors Groupe for Authentication Application", "Editors Groupes", "EditorsAuth", true }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "FkAppId", "FkMenuId", "MenuDescription", "MenuDisplayName", "MenuName", "MenuState", "MenuUrl" },
                values: new object[,]
                {
                    { new Guid("5d7d00c9-6243-453b-9300-58fc7b7d4e58"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "User setup menu.", "Users", "AuthUsers", true, "Http://srvapp/authusers" },
                    { new Guid("ab922dcb-79f3-4bfc-9c89-c6b5383ef332"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "Permissions setup menu.", "Permissions", "AuthPermissions", true, "Http://srvapp/authpermissions" },
                    { new Guid("53ad3bf3-5eba-454c-9090-d51927717cb9"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "Roles setup menu.", "Roles", "AuthRoles", true, "Http://srvapp/authroles" },
                    { new Guid("57776049-9e03-4ab0-93d6-d82ceec46d7a"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "Applications setup menu.", "Applications", "AuthApplications", true, "Http://srvapp/authappications" },
                    { new Guid("1cdbc617-e148-4cc1-846d-4f162e66ae88"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "Menus setup menu.", "Menus", "AuthMenus", true, "Http://srvapp/authmenus" },
                    { new Guid("1537cb14-9706-47be-a1cd-560ac9fdb98a"), new Guid("89221138-254c-4212-b743-c4c1b2909fb8"), null, "Groupes setup menu.", "Groupes", "AuthGroupes", true, "Http://srvapp/authgroupes" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("5bf9211a-df6c-4f3f-a059-652fc2194f33"));

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("b1335dae-e24d-4066-b8ee-246cbdfbb634"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("70744253-0bd2-4a26-b549-be8568df9bdd"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("c0f011a4-dd60-4599-99d4-464b1272fe99"));

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "GrpId",
                keyValue: new Guid("e3459c31-dbff-4bc7-b7d8-974fb16d18f4"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("1537cb14-9706-47be-a1cd-560ac9fdb98a"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("1cdbc617-e148-4cc1-846d-4f162e66ae88"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("53ad3bf3-5eba-454c-9090-d51927717cb9"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("57776049-9e03-4ab0-93d6-d82ceec46d7a"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("5d7d00c9-6243-453b-9300-58fc7b7d4e58"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: new Guid("ab922dcb-79f3-4bfc-9c89-c6b5383ef332"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("0b6add34-d44a-4197-9553-7950fddaf8c1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("1fc80226-5dd8-4dc1-b1fd-602376cf27ae"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("32b7cfae-4130-4d07-962a-783ec5658307"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("51bd26d3-2765-4d4d-bb12-323081faf3ed"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("5a3583d9-034b-48e2-8815-717114cf657e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("90d0882d-1646-43c6-b243-862e0078656e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("a8cd99c7-b339-485e-a747-be4f4472fdb7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("ac65f7fc-f494-477f-9946-92ef63bc3fee"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("b1bc9f48-a943-4564-822c-dc070b2fb2b9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("c7fb5a2e-a6f3-4779-9520-59470db130a6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("d9bbc3ef-0cd0-4688-aee7-28ea203c7602"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermId",
                keyValue: new Guid("de8f86f3-a5ba-4295-9378-2561193d7c79"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("02420da9-1db8-425b-ba7e-cb88710ad90b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("2dbf8533-9b2f-4443-a5b9-d66e435fcc1e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("79839a7a-8ad5-40f3-8d40-e313b7c836a4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b7492c1c-5268-43fd-bcee-c6227c0d863a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c5f5ab50-a938-486a-88b5-bf97bef4bdfa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("e41b1a8a-7a20-4799-aaee-b836dd28c4ae"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("f676ec19-cdb6-4d6c-9415-d8f51e75438f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("f722bd9a-03c2-45f9-be59-6106b92717e0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("fa2cc2dd-547a-418d-8273-cd3a0d30af0b"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("1edc2e9f-c013-4e95-b319-b5647e086be5"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("26009217-051a-4a16-962d-2e88c311c086"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("e950be8b-64f9-4b63-a12d-da9e417a7ca8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UsersId",
                keyValue: new Guid("fe14a91b-8b81-47ee-86bc-c73802330da7"));

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "AppId",
                keyValue: new Guid("89221138-254c-4212-b743-c4c1b2909fb8"));

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
        }
    }
}
