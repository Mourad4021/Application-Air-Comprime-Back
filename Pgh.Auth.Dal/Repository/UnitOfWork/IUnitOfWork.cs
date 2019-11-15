using System.Threading.Tasks;
using Pgh.Auth.Dal.Repository.Interface;
using Pgh.Auth.Model.Models;

namespace Pgh.Auth.Dal.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<AffApplicationUsers> AffApplicationUsers { get; }
        IRepository<AffGroupUsers> AffGroupeUser { get; }
        IRepository<AffRoleGroupMenus> AffRoleGroupMenu { get; }
        IRepository<AffRolePermissions> AffRolePermission { get; }
        IRepository<AffRolesUsersMenus> AffRoleUsersMenus { get; }
        IRepository<Applications> Application { get; }
        IRepository<Groupes> Groupe { get; }
        IRepository<Menus> Menu { get; }
        IRepository<Permissions> Permission { get; }
        IRepository<Roles> Role { get; }
        IRepository<Users> Users { get; }
       

        Task<int> Complete();
    }
}