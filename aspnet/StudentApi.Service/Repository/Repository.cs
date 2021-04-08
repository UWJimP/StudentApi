using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentApi.Abstracts;

namespace StudentApi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : AEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        
        public Repository(StudentContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(true);
        }

        public virtual async Task DeleteAsync(long id)
        {
            _dbSet.Remove((await SelectAsync(e => e.EntityID == id)).FirstOrDefault());
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync()
        {
            return await LoadAsync(await _dbSet.ToListAsync());
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await LoadAsync(await _dbSet.Where(predicate).ToListAsync().ConfigureAwait(true));
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        private async Task<IEnumerable<TEntity>> LoadAsync(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                foreach(var navigation in _dbSet.Attach(entity).Navigations)
                {
                    await navigation.LoadAsync();
                }
            }

            return entities;
        }
    }
}
