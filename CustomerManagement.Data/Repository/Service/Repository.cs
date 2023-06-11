using System;
using System.Linq.Expressions;
using CustomerManagement.Data.Models;
using CustomerManagement.Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data.Repository.Service;

public partial class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly ApplicationDbContext context;
    private readonly DbSet<T> entities;

    public Repository(ApplicationDbContext context)
    {
        this.context = context;
        entities = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await entities.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await entities.AsNoTracking().Where(expression).ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(long id, bool includeChildren = false)
    {
        return await entities.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
        await entities.AddAsync(entity);
    }

    public async Task InsertRangeAsync(IEnumerable<T> entityList)
    {
        if (entityList == null)
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
        await entities.AddRangeAsync(entityList);
    }

    public void UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
        context.Entry(entity).State = EntityState.Modified;
        // await context.SaveChangesAsync();
    }

    public void DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
        entities.Remove(entity);
        // await context.SaveChangesAsync();
    }

    public void DeleteRangeAsync(IEnumerable<T> entityList)
    {
        if (entityList == null)
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
        entities.RemoveRange(entityList);
        // await context.SaveChangesAsync();
    }
}
