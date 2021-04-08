using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StudentApi.Abstracts;

namespace StudentApi.Repository
{
    public interface IRepository<TEntity> where TEntity : AEntity
    {
        Task DeleteAsync(long id);

        Task AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> SelectAsync();

        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);
    }
}
