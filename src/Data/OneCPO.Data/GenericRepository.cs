using Microsoft.EntityFrameworkCore;
using OneCPO.Data.Common.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneCPO.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly OneCPODbContext dbContext;
        private DbSet<TEntity> dbSet;

        public GenericRepository(OneCPODbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this.dbContext.Add(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
    }
}