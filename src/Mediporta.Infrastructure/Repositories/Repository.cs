using Mediporta.Domain;
using Mediporta.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AttachRange(entities);
        _context.SaveChanges();
    }

    public IEnumerable<TEntity> GetMany()
    {
        return _dbSet;
    }

    public void ExecuteSqlRaw(string sql)
    {
        _context.Database.ExecuteSqlRaw(sql);
    }
}