using Pgh.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Pgh.Auth.Dal.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        Task<TEntity> Get(object arg);

        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> condition);

      

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entites);
        

        
        //Task<TEntity> Find(Expression<Func<TEntity, bool>> condition);
        
        Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters);
        Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters,params string[] includes);
        Task<PagedList<TEntity>> GetList(ResourceParameters resourceParameters, Expression<Func<TEntity, bool>> condition,
            params string[] includes);


        Task<IEnumerable<TEntity>> ExecuteStoreQuery(String commandText, params object[] parameters);

        Task<IEnumerable<TEntity>> ExecuteStoreQuery(String commandText, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includes = null);

    }
}