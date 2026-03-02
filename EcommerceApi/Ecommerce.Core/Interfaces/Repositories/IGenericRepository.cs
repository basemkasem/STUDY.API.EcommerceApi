using Ecommerce.Core.Interfaces.Common;

namespace Ecommerce.Core.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(ISoftDeletable entity);
}