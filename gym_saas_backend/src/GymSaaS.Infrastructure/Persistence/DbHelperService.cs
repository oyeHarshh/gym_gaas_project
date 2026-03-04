using System.Linq.Expressions;
using GymSaaS.Core.Interfaces;
using GymSaaS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GymSaaS.Infrastructure.Persistence;

public class DbHelperService : IDbHelperService
{
    private readonly AppDbContext _db;

    public DbHelperService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity =>
        await _db.Set<T>().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

    public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>>? filter = null) where T : BaseEntity
    {
        var query = _db.Set<T>().Where(e => !e.IsDeleted);
        if (filter != null)
            query = query.Where(filter);
        return await query.ToListAsync();
    }

    public async Task<T?> FindAsync<T>(Expression<Func<T, bool>> filter) where T : BaseEntity =>
        await _db.Set<T>().Where(e => !e.IsDeleted).FirstOrDefaultAsync(filter);

    public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> filter) where T : BaseEntity =>
        await _db.Set<T>().Where(e => !e.IsDeleted).AnyAsync(filter);

    public async Task<int> CountAsync<T>(Expression<Func<T, bool>>? filter = null) where T : BaseEntity
    {
        var query = _db.Set<T>().Where(e => !e.IsDeleted);
        if (filter != null)
            query = query.Where(filter);
        return await query.CountAsync();
    }

    public async Task InsertAsync<T>(T entity) where T : BaseEntity
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        await _db.Set<T>().AddRangeAsync(entities);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(T entity) where T : BaseEntity
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        _db.Set<T>().UpdateRange(entities);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(Guid id) where T : BaseEntity
    {
        var entity = await GetByIdAsync<T>(id);
        if (entity != null)
        {
            entity.SoftDelete();
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : BaseEntity
    {
        foreach (var id in ids)
        {
            var entity = await GetByIdAsync<T>(id);
            entity?.SoftDelete();
        }
        await _db.SaveChangesAsync();
    }

    public IQueryable<T> Query<T>() where T : BaseEntity =>
        _db.Set<T>().Where(e => !e.IsDeleted);

    public async Task SaveAsync() =>
        await _db.SaveChangesAsync();
}
