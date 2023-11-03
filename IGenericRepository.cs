public interface IGenericRepository<TEntity> where TEntity : class
{
}

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
}