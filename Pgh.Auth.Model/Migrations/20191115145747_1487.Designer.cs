﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pgh.Auth.Model.Models;

namespace Pgh.Auth.Model.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20191115145747_1487")]
    partial class _1487
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffApplicationUsers", b =>
                {
                    b.Property<Guid>("AppId");

                    b.Property<Guid>("UsersId");

                    b.Property<string>("Password");

                    b.HasKey("AppId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AffApplicationUsers");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffGroupUsers", b =>
                {
                    b.Property<Guid>("GrpId");

                    b.Property<Guid>("UsersId");

                    b.HasKey("GrpId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AffGroupUsers");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRoleGroupMenus", b =>
                {
                    b.Property<Guid>("GrpId");

                    b.Property<Guid>("MenuId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("GrpId", "MenuId", "RoleId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("AffRoleGroupMenus");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRolePermissions", b =>
                {
                    b.Property<Guid>("PermId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("PermId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AffRolePermissions");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRolesUsersMenus", b =>
                {
                    b.Property<Guid>("UsersId");

                    b.Property<Guid>("MenuId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UsersId", "MenuId", "RoleId");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("AffRolesUsersMenus");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Applications", b =>
                {
                    b.Property<Guid>("AppId");

                    b.Property<string>("AppCode");

                    b.Property<string>("AppDescription");

                    b.Property<string>("AppDisplayName");

                    b.Property<string>("AppName");

                    b.Property<bool>("AppState");

                    b.HasKey("AppId");

                    b.HasIndex("AppCode")
                        .IsUnique()
                        .HasFilter("([AppCode] IS NOT NULL)");

                    b.ToTable("Applications");

                    b.HasData(
                        new
                        {
                            AppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            AppCode = "0000",
                            AppDescription = "Cette application gérer l'authentification et des permissions des différents applications.",
                            AppDisplayName = "Gestion de l'authentification  des applications",
                            AppName = "AuthApp",
                            AppState = true
                        },
                        new
                        {
                            AppId = new Guid("5bf9211a-df6c-4f3f-a059-652fc2194f33"),
                            AppCode = "0001",
                            AppDescription = "Cette application gérer le processus d'analyse des échantillons par le labo Dick.",
                            AppDisplayName = "Gestion de laboratoire Dick",
                            AppName = "LaboDickAgro",
                            AppState = true
                        },
                        new
                        {
                            AppId = new Guid("b1335dae-e24d-4066-b8ee-246cbdfbb634"),
                            AppCode = "0013",
                            AppDescription = "Mise a jour de l'ancienne application Laboratoir Dick.",
                            AppDisplayName = "Gestion de laboratoire Dick Elevage",
                            AppName = "LaboDickElevage",
                            AppState = true
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Filiale", b =>
                {
                    b.Property<Guid>("FilialeID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Code");

                    b.Property<string>("Conformite_d_Exploitation");

                    b.Property<string>("Nom");

                    b.HasKey("FilialeID");

                    b.ToTable("Filiale");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Groupes", b =>
                {
                    b.Property<Guid>("GrpId");

                    b.Property<Guid?>("FkAppId");

                    b.Property<string>("GrpDescription");

                    b.Property<string>("GrpDisplayName");

                    b.Property<string>("GrpName");

                    b.Property<bool>("GrpState");

                    b.HasKey("GrpId");

                    b.HasIndex("FkAppId");

                    b.ToTable("Groupes");

                    b.HasData(
                        new
                        {
                            GrpId = new Guid("c0f011a4-dd60-4599-99d4-464b1272fe99"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            GrpDescription = "Administration Groupe for Authentication Application",
                            GrpDisplayName = "Administration Groupes",
                            GrpName = "AdministratorAuth",
                            GrpState = true
                        },
                        new
                        {
                            GrpId = new Guid("70744253-0bd2-4a26-b549-be8568df9bdd"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            GrpDescription = "Readers Groupe for Authentication Application",
                            GrpDisplayName = "Readers Groupes",
                            GrpName = "ReadersAuth",
                            GrpState = true
                        },
                        new
                        {
                            GrpId = new Guid("e3459c31-dbff-4bc7-b7d8-974fb16d18f4"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            GrpDescription = "Editors Groupe for Authentication Application",
                            GrpDisplayName = "Editors Groupes",
                            GrpName = "EditorsAuth",
                            GrpState = true
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Menus", b =>
                {
                    b.Property<Guid>("MenuId");

                    b.Property<Guid?>("FkAppId");

                    b.Property<Guid?>("FkMenuId");

                    b.Property<string>("MenuDescription");

                    b.Property<string>("MenuDisplayName");

                    b.Property<string>("MenuName");

                    b.Property<bool>("MenuState");

                    b.Property<string>("MenuUrl");

                    b.HasKey("MenuId");

                    b.HasIndex("FkAppId");

                    b.HasIndex("FkMenuId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            MenuId = new Guid("5d7d00c9-6243-453b-9300-58fc7b7d4e58"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "User setup menu.",
                            MenuDisplayName = "Users",
                            MenuName = "AuthUsers",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authusers"
                        },
                        new
                        {
                            MenuId = new Guid("ab922dcb-79f3-4bfc-9c89-c6b5383ef332"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "Permissions setup menu.",
                            MenuDisplayName = "Permissions",
                            MenuName = "AuthPermissions",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authpermissions"
                        },
                        new
                        {
                            MenuId = new Guid("53ad3bf3-5eba-454c-9090-d51927717cb9"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "Roles setup menu.",
                            MenuDisplayName = "Roles",
                            MenuName = "AuthRoles",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authroles"
                        },
                        new
                        {
                            MenuId = new Guid("57776049-9e03-4ab0-93d6-d82ceec46d7a"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "Applications setup menu.",
                            MenuDisplayName = "Applications",
                            MenuName = "AuthApplications",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authappications"
                        },
                        new
                        {
                            MenuId = new Guid("1cdbc617-e148-4cc1-846d-4f162e66ae88"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "Menus setup menu.",
                            MenuDisplayName = "Menus",
                            MenuName = "AuthMenus",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authmenus"
                        },
                        new
                        {
                            MenuId = new Guid("1537cb14-9706-47be-a1cd-560ac9fdb98a"),
                            FkAppId = new Guid("89221138-254c-4212-b743-c4c1b2909fb8"),
                            MenuDescription = "Groupes setup menu.",
                            MenuDisplayName = "Groupes",
                            MenuName = "AuthGroupes",
                            MenuState = true,
                            MenuUrl = "Http://srvapp/authgroupes"
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Permissions", b =>
                {
                    b.Property<Guid>("PermId");

                    b.Property<string>("PermDescription");

                    b.Property<string>("PermDisplayName");

                    b.Property<string>("PermName");

                    b.Property<bool>("PermState");

                    b.HasKey("PermId");

                    b.HasIndex("PermName")
                        .IsUnique()
                        .HasFilter("([PermName] IS NOT NULL)");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            PermId = new Guid("d9bbc3ef-0cd0-4688-aee7-28ea203c7602"),
                            PermDescription = "Users Will Have the permission to add items",
                            PermDisplayName = "Add Items",
                            PermName = "AddItems",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("0b6add34-d44a-4197-9553-7950fddaf8c1"),
                            PermDescription = "Users Will Have Permission to edit Items.",
                            PermDisplayName = "Edit Items",
                            PermName = "EditItems",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("51bd26d3-2765-4d4d-bb12-323081faf3ed"),
                            PermDescription = "Users Will Have Permission to delete Elements.",
                            PermDisplayName = "Delete Items",
                            PermName = "DeleteItems",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("1fc80226-5dd8-4dc1-b1fd-602376cf27ae"),
                            PermDescription = "Users Will Have Permission to View Items.",
                            PermDisplayName = "View Items",
                            PermName = "ViewItems",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("a8cd99c7-b339-485e-a747-be4f4472fdb7"),
                            PermDescription = "Users Will Have Permission to approve items.",
                            PermDisplayName = "Approve Items",
                            PermName = "ApproveItems",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("de8f86f3-a5ba-4295-9378-2561193d7c79"),
                            PermDescription = "Users Will Have Permission to show versions.",
                            PermDisplayName = "Show Versions",
                            PermName = "ShowVersions",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("c7fb5a2e-a6f3-4779-9520-59470db130a6"),
                            PermDescription = "Users Will Have Permission to Delete versions.",
                            PermDisplayName = "Delete Versions",
                            PermName = "DeleteVersions",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("5a3583d9-034b-48e2-8815-717114cf657e"),
                            PermDescription = "Users Will Have Permission to view application pages.",
                            PermDisplayName = "View application pages",
                            PermName = "ViewApplicationPages",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("90d0882d-1646-43c6-b243-862e0078656e"),
                            PermDescription = "Users Will Have Permission to create groups.",
                            PermDisplayName = "Create Groups",
                            PermName = "CreateGroups",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("32b7cfae-4130-4d07-962a-783ec5658307"),
                            PermDescription = "Users Will Have Permission to view pages.",
                            PermDisplayName = "View Pages",
                            PermName = "ViewPages",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("ac65f7fc-f494-477f-9946-92ef63bc3fee"),
                            PermDescription = "Users Will Have Permission to edit users personal information.",
                            PermDisplayName = "Edit user's personal information",
                            PermName = "EditUserPersonalInformation",
                            PermState = true
                        },
                        new
                        {
                            PermId = new Guid("b1bc9f48-a943-4564-822c-dc070b2fb2b9"),
                            PermDescription = "Users Will Have Permission to Manage personal views.",
                            PermDisplayName = "Manage personal views",
                            PermName = "ManagePersonalViews",
                            PermState = true
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Roles", b =>
                {
                    b.Property<Guid>("RoleId");

                    b.Property<string>("RoleDescription");

                    b.Property<string>("RoleDisplayName");

                    b.Property<string>("RoleName");

                    b.Property<bool>("RoleState");

                    b.HasKey("RoleId");

                    b.HasIndex("RoleName")
                        .IsUnique()
                        .HasFilter("([RoleName] IS NOT NULL)");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("f722bd9a-03c2-45f9-be59-6106b92717e0"),
                            RoleDescription = "Total Control Default Groupe",
                            RoleDisplayName = "Total Control",
                            RoleName = "TotalControl",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("02420da9-1db8-425b-ba7e-cb88710ad90b"),
                            RoleDescription = "Design Groupe Default Groupe",
                            RoleDisplayName = "Design Groupe",
                            RoleName = "Design",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("fa2cc2dd-547a-418d-8273-cd3a0d30af0b"),
                            RoleDescription = "Editors Groupe Default Groupe",
                            RoleDisplayName = "Editors Groupe",
                            RoleName = "Editors",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("79839a7a-8ad5-40f3-8d40-e313b7c836a4"),
                            RoleDescription = "Collaboration Default Groupe",
                            RoleDisplayName = "Collaboration Groupe",
                            RoleName = "Collaboration",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("c5f5ab50-a938-486a-88b5-bf97bef4bdfa"),
                            RoleDescription = "Readers Groupe Default Groupe",
                            RoleDisplayName = "Readers Groupe",
                            RoleName = "Readers",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("e41b1a8a-7a20-4799-aaee-b836dd28c4ae"),
                            RoleDescription = "Limited Access Default Groupe",
                            RoleDisplayName = "Limited Access",
                            RoleName = "LimitedAccess",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("f676ec19-cdb6-4d6c-9415-d8f51e75438f"),
                            RoleDescription = "Display Only Default Groupe",
                            RoleDisplayName = "Display Only Groupe",
                            RoleName = "DisplayOnly",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("2dbf8533-9b2f-4443-a5b9-d66e435fcc1e"),
                            RoleDescription = "Approval Default Groupe",
                            RoleDisplayName = "Approval Groupe",
                            RoleName = "Approval",
                            RoleState = true
                        },
                        new
                        {
                            RoleId = new Guid("b7492c1c-5268-43fd-bcee-c6227c0d863a"),
                            RoleDescription = "Restricted reading Default Groupe",
                            RoleDisplayName = "Restricted reading Groupe",
                            RoleName = "Restricted reading",
                            RoleState = true
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Users", b =>
                {
                    b.Property<Guid>("UsersId");

                    b.Property<Guid?>("FilialeID");

                    b.Property<Guid?>("FkUsersId");

                    b.Property<DateTime>("UsersBirthDate");

                    b.Property<string>("UsersCode")
                        .HasMaxLength(8);

                    b.Property<DateTime>("UsersDateLeave");

                    b.Property<string>("UsersGenderCode");

                    b.Property<DateTime>("UsersJoinDate");

                    b.Property<string>("UsersLastName")
                        .HasMaxLength(50);

                    b.Property<string>("UsersMail")
                        .HasMaxLength(80);

                    b.Property<string>("UsersMailIntern")
                        .HasMaxLength(80);

                    b.Property<string>("UsersName")
                        .HasMaxLength(50);

                    b.Property<string>("UsersPersonalNumber");

                    b.Property<string>("UsersPhoneNumber");

                    b.Property<string>("UsersPosteName");

                    b.Property<bool>("UsersState");

                    b.HasKey("UsersId");

                    b.HasIndex("FilialeID");

                    b.HasIndex("FkUsersId");

                    b.HasIndex("UsersCode")
                        .IsUnique()
                        .HasFilter("([UsersCode] IS NOT NULL)");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UsersId = new Guid("26009217-051a-4a16-962d-2e88c311c086"),
                            UsersBirthDate = new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(781),
                            UsersCode = "00000000",
                            UsersDateLeave = new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(9376),
                            UsersGenderCode = "M",
                            UsersJoinDate = new DateTime(2019, 11, 15, 15, 57, 47, 55, DateTimeKind.Local).AddTicks(9849),
                            UsersLastName = "Admin",
                            UsersMail = "Admin@poulina.com",
                            UsersMailIntern = "Admin@poulina.com",
                            UsersName = "Admin",
                            UsersPersonalNumber = "63524163",
                            UsersPhoneNumber = "63524141",
                            UsersPosteName = "Admin Poste",
                            UsersState = false
                        },
                        new
                        {
                            UsersId = new Guid("1edc2e9f-c013-4e95-b319-b5647e086be5"),
                            UsersBirthDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3396),
                            UsersCode = "00000001",
                            UsersDateLeave = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3400),
                            UsersGenderCode = "M",
                            UsersJoinDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3404),
                            UsersLastName = "SupAdmin",
                            UsersMail = "SupAdmin@poulina.com",
                            UsersMailIntern = "SupAdmin@poulina.com",
                            UsersName = "SupAdmin",
                            UsersPersonalNumber = "63524163",
                            UsersPhoneNumber = "63524141",
                            UsersPosteName = "SupAdmin Poste",
                            UsersState = false
                        },
                        new
                        {
                            UsersId = new Guid("e950be8b-64f9-4b63-a12d-da9e417a7ca8"),
                            UsersBirthDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3428),
                            UsersCode = "00000002",
                            UsersDateLeave = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3428),
                            UsersGenderCode = "M",
                            UsersJoinDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3429),
                            UsersLastName = "Test",
                            UsersMail = "User1@poulina.com",
                            UsersMailIntern = "User1@poulina.com",
                            UsersName = "User1",
                            UsersPersonalNumber = "63524163",
                            UsersPhoneNumber = "63524141",
                            UsersPosteName = "User1 Poste",
                            UsersState = false
                        },
                        new
                        {
                            UsersId = new Guid("fe14a91b-8b81-47ee-86bc-c73802330da7"),
                            UsersBirthDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3432),
                            UsersCode = "00000003",
                            UsersDateLeave = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3432),
                            UsersGenderCode = "M",
                            UsersJoinDate = new DateTime(2019, 11, 15, 15, 57, 47, 56, DateTimeKind.Local).AddTicks(3433),
                            UsersLastName = "Test",
                            UsersMail = "User2@poulina.com",
                            UsersMailIntern = "User2@poulina.com",
                            UsersName = "User2",
                            UsersPersonalNumber = "63524163",
                            UsersPhoneNumber = "63524141",
                            UsersPosteName = "User2 Poste",
                            UsersState = false
                        });
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffApplicationUsers", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Applications", "App")
                        .WithMany("AffApplicationUsers")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Users", "Users")
                        .WithMany("AffApplicationUsers")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffGroupUsers", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Groupes", "Grp")
                        .WithMany("AffGroupUsers")
                        .HasForeignKey("GrpId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Users", "Users")
                        .WithMany("AffGroupUsers")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRoleGroupMenus", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Groupes", "Groupe")
                        .WithMany("AffRoleGroupMenus")
                        .HasForeignKey("GrpId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Menus", "Menu")
                        .WithMany("AffRoleGroupMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Roles", "Role")
                        .WithMany("AffRoleGroupMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRolePermissions", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Permissions", "Permission")
                        .WithMany("AffRolePermissions")
                        .HasForeignKey("PermId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Roles", "Role")
                        .WithMany("AffRolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.AffRolesUsersMenus", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Menus", "Menu")
                        .WithMany("AffRolesUsersMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Roles", "Role")
                        .WithMany("AffRolesUsersMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pgh.Auth.Model.Models.Users", "Users")
                        .WithMany("AffRolesUsersMenus")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Groupes", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Applications", "FkApp")
                        .WithMany("Groupes")
                        .HasForeignKey("FkAppId")
                        .HasConstraintName("FK_Groupes_Applications")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Menus", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Applications", "FkApp")
                        .WithMany("Menus")
                        .HasForeignKey("FkAppId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Pgh.Auth.Model.Models.Menus", "FkMenu")
                        .WithMany("InverseFkMenu")
                        .HasForeignKey("FkMenuId");
                });

            modelBuilder.Entity("Pgh.Auth.Model.Models.Users", b =>
                {
                    b.HasOne("Pgh.Auth.Model.Models.Filiale")
                        .WithMany("Users")
                        .HasForeignKey("FilialeID");

                    b.HasOne("Pgh.Auth.Model.Models.Users", "FkUsers")
                        .WithMany("InverseFkUsers")
                        .HasForeignKey("FkUsersId");
                });
#pragma warning restore 612, 618
        }
    }
}