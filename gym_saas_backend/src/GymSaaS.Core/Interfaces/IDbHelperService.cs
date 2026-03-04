using System.Linq.Expressions;
using GymSaaS.Domain.Common;

namespace GymSaaS.Core.Interfaces;

public interface IDbHelperService
{
    // Read
    Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity;
    Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>>? filter = null) where T : BaseEntity;
    Task<T?> FindAsync<T>(Expression<Func<T, bool>> filter) where T : BaseEntity;
    Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> filter) where T : BaseEntity;
    Task<int> CountAsync<T>(Expression<Func<T, bool>>? filter = null) where T : BaseEntity;

    // Write
    Task InsertAsync<T>(T entity) where T : BaseEntity;
    Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
    Task UpdateAsync<T>(T entity) where T : BaseEntity;
    Task UpdateAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
    Task DeleteAsync<T>(Guid id) where T : BaseEntity;
    Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : BaseEntity;

    // Flexible querying for complex cases (Include, OrderBy, joins etc.)
    IQueryable<T> Query<T>() where T : BaseEntity;

    Task SaveAsync();
}
