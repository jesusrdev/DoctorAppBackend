using System.Linq.Expressions;

namespace Data.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null //  Include ['', '', '']
    );

    Task<T> GetFirst(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null
    );

    Task Add(T entity);

    void Remove(T entity);
}