using GranTitan.DAL.Entities;
using System.Linq.Expressions;

namespace GranTitan.DAL.Interface
{
    public interface IRepository<T> where T : DomainEntity
    {
        Task<T> GetOneAsync(Guid id, string? includeStringProperties = default);
        Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeStringProperties = "",
            bool isTracking = false);
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

        Task<int> GetCountAsync();

    }
}
