using Microsoft.EntityFrameworkCore;
using Pgh.Auth.Dal.Repository.Interface;
using Pgh.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Principal;

namespace Pgh.Auth.Dal.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        #region Create Function
        
        public virtual async Task Add(TEntity entity)
        {
            try
            {
                await Context.Set<TEntity>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await Context.Set<TEntity>().AddRangeAsync(entities);
            }
            catch (Exception e)
            {
                var s = e.ToString();
                throw;
            }

        }
        
        #endregion

        #region Read  Function

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                IQueryable<TEntity> query = Context.Set<TEntity>();

                if (includes != null)
                {
                    query = includes(query);
                }

                return await query.FirstOrDefaultAsync(condition);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> condition)
        {
           
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            if (includes == null) throw new ArgumentNullException(nameof(includes));
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.Where(condition).ToListAsync();
        }
        
        public virtual async Task<TEntity> Get(object arg)
        {
            try
            {
                return await Context.Set<TEntity>().FindAsync(arg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


       

        #endregion

        #region Remove function

        public void Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
            }
        }


        public void RemoveRange(IEnumerable<TEntity> entites)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entites);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        
        #endregion

       

        #region Pagination

        public virtual async Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters)
        {

            return await PagedList<TEntity>
                .Create(Context.Set<TEntity>(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }


        public virtual async Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters,
            params string[] includes)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>();

            foreach (var include in includes)
                query = query.Include(include);

            return await PagedList<TEntity>
                .Create(query, resourceParameters.PageNumber, resourceParameters.PageSize);
        }

        public virtual async Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters, 
            Expression<Func<TEntity, bool>> condition,
            params string[] includes)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>();

            foreach (var include in includes)
                query = query.Include(include);
            
            return await PagedList<TEntity>
                .Create(query.Where(condition), resourceParameters.PageNumber, resourceParameters.PageSize);
        }

        public virtual IEnumerable<TEntity> GetList(params string[] includes)
        {
            
            return Context.Set<TEntity>().ToList();
        }



        #endregion


        #region StoredProcedure

        public virtual async Task<IEnumerable<TEntity>> ExecuteStoreQuery(String commandText, params object[] parameters)
        {
            try
            {
                return await Context.Set<TEntity>().FromSql(commandText, parameters).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> ExecuteStoreQuery(String commandText, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                return await Context.Set<TEntity>().FromSql(commandText, includes).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //TO modify error not bringing Foreign Values.
        //public virtual async Task<PagedList<TEntity>> GetListStoredProcedures(ResourceParameters resourceParameters,
        //    String commandText,Expression<Func<TEntity, bool>> condition,
        //    params string[] includes)
        //{

        //    IQueryable<TEntity> query = Context.Set<TEntity>();

        //    foreach (var include in includes)
        //        query = query.Include(include);

        //    return await PagedList<TEntity>
        //        .Create(query.FromSql(commandText),
        //            resourceParameters.PageNumber, resourceParameters.PageSize);
        //}

        #endregion

    }
}