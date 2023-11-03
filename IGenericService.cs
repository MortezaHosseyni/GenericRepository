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

public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        return _mapper.Map<TDto>(await _repository.GetByIdAsync(id));
    }

    public async Task<TDto> GetByGuidAsync(string guid)
    {
        return _mapper.Map<TDto>(await _repository.GetByGuidAsync(guid));
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<TDto>>(await _repository.GetAllAsync());
    }

    public async Task<IEnumerable<TDto>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var entities = await _repository.GetAllWithIncludeAsync(includes);
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<IEnumerable<TDto>> GetLimitedDataOrderedAsync(int limit, string orderByPropertyName)
    {
        var entityList = await _repository.GetLimitedDataAsync(limit, orderByPropertyName);
        return _mapper.Map<IEnumerable<TDto>>(entityList);
    }

    public async Task<IEnumerable<TDto>> FindAsync(Expression<Func<TDto, bool>> predicate)
    {
        Expression<Func<TEntity, bool>> entityExpression = predicate.ConvertDtoToEntity<TDto, TEntity>();

        return _mapper.Map<IEnumerable<TDto>>(await _repository.FindAsync(entityExpression));
    }

    public async Task<IEnumerable<TDto>> FindInfinityAsync(Expression<Func<TDto, bool>> predicate, int skip, int take)
    {
        Expression<Func<TEntity, bool>> entityExpression = predicate.ConvertDtoToEntity<TDto, TEntity>();

        return _mapper.Map<IEnumerable<TDto>>(await _repository.FindInfinityAsync(entityExpression, skip, take));
    }

    public async Task<TDto> FindOneAsync(Expression<Func<TDto, bool>> predicate)
    {
        Expression<Func<TEntity, bool>> entityExpression = predicate.ConvertDtoToEntity<TDto, TEntity>();

        return _mapper.Map<TDto>(await _repository.FindOneAsync(entityExpression));
    }

    public async Task<long> CountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<long> CountByPredicateAsync(Expression<Func<TDto, bool>> predicate)
    {
        Expression<Func<TEntity, bool>> entityExpression = predicate.ConvertDtoToEntity<TDto, TEntity>();

        return await _repository.CountByPredicateAsync(entityExpression);
    }

    public void Add(TDto entity)
    {
        _repository.Add(_mapper.Map<TEntity>(entity));
    }

    public void AddRange(IEnumerable<TDto> entities)
    {
        _repository.AddRange(_mapper.Map<IEnumerable<TEntity>>(entities));
    }

    public void Update(TDto entity)
    {
        _repository.Update(_mapper.Map<TEntity>(entity));
    }

    public void UpdateRange(IEnumerable<TDto> entities)
    {
        _repository.UpdateRange(_mapper.Map<IEnumerable<TEntity>>(entities));
    }

    public void Remove(TDto entity)
    {
        _repository.Remove(_mapper.Map<TEntity>(entity));
    }

    public void RemoveRange(IEnumerable<TDto> entities)
    {
        _repository.RemoveRange(_mapper.Map<IEnumerable<TEntity>>(entities));
    }
}