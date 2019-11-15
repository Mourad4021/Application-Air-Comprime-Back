using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Pgh.Auth.Model.Models
{
    public class AuthDbContext : DbContext
    {
        public DbSet<Users> User { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Groupes> Groupes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get;set; }
       public DbSet<Filiale> Filiale { get; set; }

        public DbSet<AffApplicationUsers> AffApplicationUsers { get; set; }
        public DbSet<AffGroupUsers> AffGroupUsers { get; set; }
        public DbSet<AffRolePermissions> AffRolePermissions { get; set; }
        public DbSet<AffRoleGroupMenus> AffRoleGroupMenus { get; set; }
        public DbSet<AffRolesUsersMenus> AffRolesUsersMenus { get; set; }

        public AuthDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            
               
        //optionsBuilder.UseSqlServer(@"Data Source=tcp:172.21.66.129,1433;Database=Pgh.AuthService;User ID=hajour;Password=340$Uuxwp7Mcxo7Khy; MultipleActiveResultSets=True");


               optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=Pgh.Authentification;Trusted_Connection=True;MultipleActiveResultSets=true");
              //hajer // optionsBuilder.UseSqlServer(@"Server=192.168.160.57;Database=Pgh.GestionCompresseur.AuthDB;uid=sa;pwd=PgHSqL2016;MultipleActiveResultSets=True");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AffApplicationUsers>(entity =>
            {
                entity.HasKey(e => new { e.AppId, e.UsersId });

                entity.HasIndex(e => e.UsersId);

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AffApplicationUsers)
                    .HasForeignKey(d => d.AppId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.AffApplicationUsers)
                    .HasForeignKey(d => d.UsersId);
            });

            modelBuilder.Entity<AffGroupUsers>(entity =>
            {
                entity.HasKey(e => new { e.GrpId, e.UsersId });

                entity.HasIndex(e => e.UsersId);

                entity.HasOne(d => d.Grp)
                    .WithMany(p => p.AffGroupUsers)
                    .HasForeignKey(d => d.GrpId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.AffGroupUsers)
                    .HasForeignKey(d => d.UsersId);
            });

            modelBuilder.Entity<AffRoleGroupMenus>(entity =>
            {
                entity.HasKey(e => new { e.GrpId, e.MenuId, e.RoleId });

                entity.HasIndex(e => e.MenuId);

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Groupe)
                    .WithMany(p => p.AffRoleGroupMenus)
                    .HasForeignKey(d => d.GrpId);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.AffRoleGroupMenus)
                    .HasForeignKey(d => d.MenuId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AffRoleGroupMenus)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AffRolePermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AffRolePermissions)
                    .HasForeignKey(d => d.PermId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AffRolePermissions)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AffRolesUsersMenus>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.MenuId, e.RoleId });

                entity.HasIndex(e => e.MenuId);

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.AffRolesUsersMenus)
                    .HasForeignKey(d => d.MenuId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AffRolesUsersMenus)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.AffRolesUsersMenus)
                    .HasForeignKey(d => d.UsersId);
            });

            modelBuilder.Entity<Applications>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.HasIndex(e => e.AppCode)
                    .IsUnique()
                    .HasFilter("([AppCode] IS NOT NULL)");

                entity.Property(e => e.AppId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Groupes>(entity =>
            {
                entity.HasKey(e => e.GrpId);

                entity.HasIndex(e => e.FkAppId);

                entity.Property(e => e.GrpId).ValueGeneratedNever();

                entity.HasOne(d => d.FkApp)
                    .WithMany(p => p.Groupes)
                    .HasForeignKey(d => d.FkAppId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Groupes_Applications");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.HasIndex(e => e.FkMenuId);

                entity.Property(e => e.MenuId).ValueGeneratedNever();

                entity.HasOne(d => d.FkApp)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.FkAppId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.FkMenu)
                    .WithMany(p => p.InverseFkMenu)
                    .HasForeignKey(d => d.FkMenuId);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasKey(e => e.PermId);

                entity.HasIndex(e => e.PermName)
                    .IsUnique()
                    .HasFilter("([PermName] IS NOT NULL)");

                entity.Property(e => e.PermId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.RoleName)
                    .IsUnique()
                    .HasFilter("([RoleName] IS NOT NULL)");

                entity.Property(e => e.RoleId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UsersId);

                entity.HasIndex(e => e.FkUsersId);

                entity.HasIndex(e => e.UsersCode)
                    .IsUnique()
                    .HasFilter("([UsersCode] IS NOT NULL)");

                entity.Property(e => e.UsersId).ValueGeneratedNever();

                entity.Property(e => e.UsersCode).HasMaxLength(8);

                entity.Property(e => e.UsersLastName).HasMaxLength(50);

                entity.Property(e => e.UsersMail).HasMaxLength(80);

                entity.Property(e => e.UsersMailIntern).HasMaxLength(80);

                entity.Property(e => e.UsersName).HasMaxLength(50);

                entity.HasOne(d => d.FkUsers)
                    .WithMany(p => p.InverseFkUsers)
                    .HasForeignKey(d => d.FkUsersId);
            });
            
            #region SeedData

            //Initialization des different donneés

            var appId = Guid.NewGuid();
            modelBuilder.Entity<Users>()
                .HasData(
                    new Users
                    {
                        UsersId = Guid.NewGuid(),
                        UsersCode = "00000000",
                        UsersLastName = "Admin",
                        UsersName = "Admin",
                        UsersBirthDate = DateTime.Now,
                        UsersDateLeave = DateTime.Now,
                        UsersJoinDate = DateTime.Now,
                        UsersGenderCode = "M",
                        UsersMail = "Admin@poulina.com",
                        UsersMailIntern = "Admin@poulina.com",
                        UsersPersonalNumber = "63524163",
                        UsersPhoneNumber = "63524141",
                        UsersPosteName = "Admin Poste",
                        UsersState = false,
                        FkUsers = null,
                    },
                    new Users
                    {
                        UsersId = Guid.NewGuid(),
                        UsersCode = "00000001",
                        UsersLastName = "SupAdmin",
                        UsersName = "SupAdmin",
                        UsersBirthDate = DateTime.Now,
                        UsersDateLeave = DateTime.Now,
                        UsersJoinDate = DateTime.Now,
                        UsersGenderCode = "M",
                        UsersMail = "SupAdmin@poulina.com",
                        UsersMailIntern = "SupAdmin@poulina.com",
                        UsersPersonalNumber = "63524163",
                        UsersPhoneNumber = "63524141",
                        UsersPosteName = "SupAdmin Poste",
                        UsersState = false,
                        FkUsers = null,
                    },
                    new Users
                    {
                        UsersId = Guid.NewGuid(),
                        UsersCode = "00000002",
                        UsersLastName = "Test",
                        UsersName = "User1",
                        UsersBirthDate = DateTime.Now,
                        UsersDateLeave = DateTime.Now,
                        UsersJoinDate = DateTime.Now,
                        UsersGenderCode = "M",
                        UsersMail = "User1@poulina.com",
                        UsersMailIntern = "User1@poulina.com",
                        UsersPersonalNumber = "63524163",
                        UsersPhoneNumber = "63524141",
                        UsersPosteName = "User1 Poste",
                        UsersState = false,
                        FkUsers = null,
                    },
                    new Users
                    {
                        UsersId = Guid.NewGuid(),
                        UsersCode = "00000003",
                        UsersLastName = "Test",
                        UsersName = "User2",
                        UsersBirthDate = DateTime.Now,
                        UsersDateLeave = DateTime.Now,
                        UsersJoinDate = DateTime.Now,
                        UsersGenderCode = "M",
                      
                        UsersMail = "User2@poulina.com",
                        UsersMailIntern = "User2@poulina.com",
                        UsersPersonalNumber = "63524163",
                        UsersPhoneNumber = "63524141",
                        UsersPosteName = "User2 Poste",
                        UsersState = false,
                        FkUsers = null,
                    }
                );

            modelBuilder.Entity<Applications>()
                .HasData(
                    new Applications
                    {
                        AppId = appId,
                        AppCode = "0000",
                        AppName = "AuthApp",
                        AppDisplayName = "Gestion de l'authentification  des applications",
                        AppDescription = "Cette application gérer l'authentification et des permissions des différents applications.",
                        AppState = true,

                    },
                    new Applications
                    {
                        AppId = Guid.NewGuid(),
                        AppCode = "0001",
                        AppName = "LaboDickAgro",
                        AppDisplayName = "Gestion de laboratoire Dick",
                        AppDescription = "Cette application gérer le processus d'analyse des échantillons par le labo Dick.",
                        AppState = true,

                    },
                    new Applications
                    {
                        AppId = Guid.NewGuid(),
                        AppCode = "0013",
                        AppName = "LaboDickElevage",
                        AppDisplayName = "Gestion de laboratoire Dick Elevage",
                        AppDescription = "Mise a jour de l'ancienne application Laboratoir Dick.",
                        AppState = true,
                    }
                );
            
            modelBuilder.Entity<Permissions>()
                .HasData(
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "AddItems",
                        PermDisplayName = "Add Items",
                        PermDescription = "Users Will Have the permission to add items",
                        PermState = true,

                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "EditItems",
                        PermDisplayName = "Edit Items",
                        PermDescription = "Users Will Have Permission to edit Items.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "DeleteItems",
                        PermDisplayName = "Delete Items",
                        PermDescription = "Users Will Have Permission to delete Elements.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ViewItems",
                        PermDisplayName = "View Items",
                        PermDescription = "Users Will Have Permission to View Items.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ApproveItems",
                        PermDisplayName = "Approve Items",
                        PermDescription = "Users Will Have Permission to approve items.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ShowVersions",
                        PermDisplayName = "Show Versions",
                        PermDescription = "Users Will Have Permission to show versions.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "DeleteVersions",
                        PermDisplayName = "Delete Versions",
                        PermDescription = "Users Will Have Permission to Delete versions.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ViewApplicationPages",
                        PermDisplayName = "View application pages",
                        PermDescription = "Users Will Have Permission to view application pages.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "CreateGroups",
                        PermDisplayName = "Create Groups",
                        PermDescription = "Users Will Have Permission to create groups.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ViewPages",
                        PermDisplayName = "View Pages",
                        PermDescription = "Users Will Have Permission to view pages.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "EditUserPersonalInformation",
                        PermDisplayName = "Edit user's personal information",
                        PermDescription = "Users Will Have Permission to edit users personal information.",
                        PermState = true,
                    },
                    new Permissions
                    {
                        PermId = Guid.NewGuid(),
                        PermName = "ManagePersonalViews",
                        PermDisplayName = "Manage personal views",
                        PermDescription = "Users Will Have Permission to Manage personal views.",
                        PermState = true,
                    }
                );
            
            modelBuilder.Entity<Roles>()
                .HasData(
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "TotalControl",
                        RoleDisplayName = "Total Control",
                        RoleDescription = "Total Control Default Groupe",
                        RoleState = true,

                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Design",
                        RoleDisplayName = "Design Groupe",
                        RoleDescription = "Design Groupe Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Editors",
                        RoleDisplayName = "Editors Groupe",
                        RoleDescription = "Editors Groupe Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Collaboration",
                        RoleDisplayName = "Collaboration Groupe",
                        RoleDescription = "Collaboration Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Readers",
                        RoleDisplayName = "Readers Groupe",
                        RoleDescription = "Readers Groupe Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "LimitedAccess",
                        RoleDisplayName = "Limited Access",
                        RoleDescription = "Limited Access Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "DisplayOnly",
                        RoleDisplayName = "Display Only Groupe",
                        RoleDescription = "Display Only Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Approval",
                        RoleDisplayName = "Approval Groupe",
                        RoleDescription = "Approval Default Groupe",
                        RoleState = true,
                    },
                    new Roles
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Restricted reading",
                        RoleDisplayName = "Restricted reading Groupe",
                        RoleDescription = "Restricted reading Default Groupe",
                        RoleState = true,
                    }
                );
            
            modelBuilder.Entity<Menus>()
                .HasData(
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthUsers",
                    MenuUrl = "Http://srvapp/authusers",
                    MenuDisplayName = "Users",
                    MenuDescription = "User setup menu.",
                    MenuState = true,
                    FkMenuId = null
                },
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthPermissions",
                    MenuUrl = "Http://srvapp/authpermissions",
                    MenuDisplayName = "Permissions",
                    MenuDescription = "Permissions setup menu.",
                    MenuState = true,
                    FkMenuId = null
                },
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthRoles",
                    MenuUrl = "Http://srvapp/authroles",
                    MenuDisplayName = "Roles",
                    MenuDescription = "Roles setup menu.",
                    MenuState = true,
                    FkMenuId = null
                },
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthApplications",
                    MenuUrl = "Http://srvapp/authappications",
                    MenuDisplayName = "Applications",
                    MenuDescription = "Applications setup menu.",
                    MenuState = true,
                    FkMenuId = null
                },
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthMenus",
                    MenuUrl = "Http://srvapp/authmenus",
                    MenuDisplayName = "Menus",
                    MenuDescription = "Menus setup menu.",
                    MenuState = true,
                    FkMenuId = null
                },
                new Menus
                {
                    MenuId = Guid.NewGuid(),
                    FkAppId = appId,
                    MenuName = "AuthGroupes",
                    MenuUrl = "Http://srvapp/authgroupes",
                    MenuDisplayName = "Groupes",
                    MenuDescription = "Groupes setup menu.",
                    MenuState = true,
                    FkMenuId = null
                }
                );

            modelBuilder.Entity<Groupes>().HasData(
                new Groupes
                {
                    GrpId = Guid.NewGuid(),
                    GrpName = "AdministratorAuth",
                    GrpDisplayName = "Administration Groupes",
                    GrpState = true,
                    GrpDescription = "Administration Groupe for Authentication Application",
                    FkAppId = appId
                }, 
                new Groupes
                {
                    GrpId = Guid.NewGuid(),
                    GrpName = "ReadersAuth",
                    GrpDisplayName = "Readers Groupes",
                    GrpState = true,
                    GrpDescription = "Readers Groupe for Authentication Application",
                    FkAppId = appId
                }, 
                new Groupes
                {
                    GrpId = Guid.NewGuid(),
                    GrpName = "EditorsAuth",
                    GrpDisplayName = "Editors Groupes",
                    GrpState = true,
                    GrpDescription = "Editors Groupe for Authentication Application",
                    FkAppId = appId
                }
                );

            #endregion
        }

    }
}