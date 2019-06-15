using System.Linq;

namespace OneCPO.Data.Common.Contracts
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        void Add(TEntity entity);

        void Delete(TEntity entity);

        int SaveChanges();
    }
}