using System;
using System.Threading.Tasks;
using Pgh.Auth.Dal.Repository.Implementation;
using Pgh.Auth.Dal.Repository.Interface;
using Pgh.Auth.Model.Models;

namespace Pgh.Auth.Dal.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _authDbContext;

        public IRepository<AffApplicationUsers> AffApplicationUsers { get; private set; }
        public IRepository<AffGroupUsers> AffGroupeUser { get; private set; }
        public IRepository<AffRoleGroupMenus> AffRoleGroupMenu { get; private set; }
        public IRepository<AffRolePermissions> AffRolePermission { get; private set; }
        public IRepository<AffRolesUsersMenus> AffRoleUsersMenus { get; private set; }
        public IRepository<Applications> Application { get; private set; }
        public IRepository<Groupes> Groupe { get; private set; }
        public IRepository<Menus> Menu { get; private set; }
        public IRepository<Permissions> Permission { get; private set; }
        public IRepository<Roles> Role { get; private set; }
        public IRepository<Users> Users { get; private set; }
        
        public UnitOfWork(AuthDbContext authDbContext)
        {
            if (authDbContext != null)
            {
                _authDbContext = authDbContext;

                AffApplicationUsers = new Repository<AffApplicationUsers>(_authDbContext);
                AffGroupeUser = new Repository<AffGroupUsers>(_authDbContext);
                AffRoleGroupMenu = new Repository<AffRoleGroupMenus>(_authDbContext);
                AffRolePermission = new Repository<AffRolePermissions>(_authDbContext);
                AffRoleUsersMenus =  new Repository<AffRolesUsersMenus>(_authDbContext);
                
                Application = new Repository<Applications>(_authDbContext);
                Groupe = new Repository<Groupes>(_authDbContext);
                Menu = new Repository<Menus>(_authDbContext);
                Permission = new Repository<Permissions>(_authDbContext);
                Role = new Repository<Roles>(_authDbContext);
                Users = new Repository<Users>(_authDbContext);

            }
        }

        public Task<int> Complete()
        {
            try
            {
                return _authDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public void Dispose()
        {

            _authDbContext.Dispose();
        }
    }
}