namespace Mediporta.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void AddRange(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> GetMany(); 
    void ExecuteSqlRaw(string sql);
}