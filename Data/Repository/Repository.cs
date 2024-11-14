using System.Linq.Expressions;
using Data.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class Repository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private DbSet<T> _dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var ip in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(ip);
            }
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var ip in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(ip);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}