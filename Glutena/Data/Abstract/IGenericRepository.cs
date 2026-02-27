using System.Linq.Expressions;
using Glutena.Models.Entities;

namespace Glutena.Data.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);

        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task SaveAsync();
    }
}