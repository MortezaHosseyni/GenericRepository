public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> GetByGuidAsync(string guid);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindInfinityAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetLimitedDataAsync(int limit, string orderByPropertyName);
    Task<long> CountAsync();
    Task<long> CountByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
}