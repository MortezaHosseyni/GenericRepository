public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<TDto> GetByIdAsync(int id);
    Task<TDto> GetByGuidAsync(string guid);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<IEnumerable<TDto>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TDto>> FindAsync(Expression<Func<TDto, bool>> predicate);
    Task<IEnumerable<TDto>> FindInfinityAsync(Expression<Func<TDto, bool>> predicate, int skip, int take);
    Task<TDto> FindOneAsync(Expression<Func<TDto, bool>> predicate);
    Task<IEnumerable<TDto>> GetLimitedDataOrderedAsync(int limit, string orderByPropertyName);
    Task<long> CountAsync();
    Task<long> CountByPredicateAsync(Expression<Func<TDto, bool>> predicate);
    void Add(TDto entity);
    void AddRange(IEnumerable<TDto> entities);
    void Update(TDto entity);
    void UpdateRange(IEnumerable<TDto> entities);
    void Remove(TDto entity);
    void RemoveRange(IEnumerable<TDto> entities);
}