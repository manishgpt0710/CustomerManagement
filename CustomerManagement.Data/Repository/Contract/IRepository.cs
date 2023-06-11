using System;
using System.Linq.Expressions;
using CustomerManagement.Data.Models;

namespace CustomerManagement.Data.Repository.Contract;

public interface IRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task<T?> GetByIdAsync(long id, bool includeChildren = false);
    Task InsertAsync(T entity);
    Task InsertRangeAsync(IEnumerable<T> entities);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    void DeleteRangeAsync(IEnumerable<T> entities);
}
