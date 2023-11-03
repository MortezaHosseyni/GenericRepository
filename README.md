# Generic Repository Class
The "Generic Repository" class is a versatile data access component designed to simplify and standardize database interactions in C# applications. It provides a generic implementation for common CRUD (Create, Read, Update, Delete) operations, making it easy to work with different types of entities and databases. By abstracting away the underlying data access details, it promotes clean and maintainable code, reduces code duplication, and enhances testability. Users can extend and customize this class to suit their specific data access requirements.

# Generic Service Class
The "Generic Service" class complements the "Generic Repository" by providing a higher-level service layer for your application. It encapsulates business logic and orchestrates interactions with the repository, enabling you to build a robust and maintainable application. This class can be used to define common operations, validations, and custom business logic for your application, enhancing its modularity and scalability. It promotes the separation of concerns, making it easier to manage and expand your application's functionality.

# Requirements
- Microsoft Entity Framework Core
- AutoMapper

# How to use?
1. Create Mapper Profile:
You need to create a mapper profile to map your models to DTOs and vice versa. You can use AutoMapper for this purpose. Here's an example of how to create a mapper profile:
```
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        // Add mappings for other models and DTOs as needed.
    }
}
```

2. Add GenericRepository to Data Access Layer:
Add the "GenericRepository" class to your Data Access Layer, and make sure it uses your own database context (replace 'AppDbContext' with your 'ContextDb').

3. Add GenericService to Business Logic Layer:
Similarly, add the "GenericService" class to your Business Logic Layer.

4. Register Services in Program.cs:
In your Program.cs (or Startup.cs if it's an ASP.NET Core application), register the repository and service classes with the dependency injection container as follows:
```
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
```

5. Import Your Own ContextDb:
In the constructor of the "GenericRepository" class, import your own database context (ContextDb) instead of 'AppDbContext':
```
protected readonly YourContextDb _context;

public GenericRepository(YourContextDb context)
{
    _context = context;
}
```

6. Create Repository and Service for Your Model:
For each model (e.g., 'User'), create a repository and service that inherit from the generic classes. Here's an example for the 'User' model:

User Model Repository:
```
public interface IUserRepository : IGenericRepository<User>
{
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ContextDb _context;
    public UserRepository(ContextDb context) : base(context)
    {
        _context = context;
    }
}
```

User Model Service:
```
public interface IUserService : IGenericService<User, UserDTO>
{
}

public class UserService : GenericService<User, UserDTO>, IUserService
{
    public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
    {
    }
}
```

With these steps, you've effectively set up your "Generic Repository" and "Generic Service" classes and created specific repository and service classes for your models. This approach helps you manage data access and business logic efficiently while maintaining a clean and structured codebase.