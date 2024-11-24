namespace Taller1.Mapper;

/// <summary>
/// Factory class that manages the creation and retrieval of object mappers.
/// It stores mappings between different object types (TObject to TResult) and provides a method to get the appropriate mapper.
/// </summary>
public class MapperFactory : IMapperFactory
{

    private readonly IDictionary<Tuple<Type, Type>, object> _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapperFactory"/> class.
    /// It sets up the initial mappers for different object type conversions.
    /// </summary>
    public MapperFactory()
    {
        _factory = new Dictionary<Tuple<Type, Type>, object>();
        
        NewMapper(new ToProductMapper());
        NewMapper(new ToProductView());
        NewMapper(new ToUserMapper());
        NewMapper(new ToUserViewMapper());
        NewMapper(new ToVoucherMapper());
        NewMapper(new ToVoucherViewMapper());
    }

    /// <summary>
    /// Adds a new object mapper to the factory.
    /// The mapper is stored based on the source and destination object types.
    /// </summary>
    /// <typeparam name="TObject">The source object type.</typeparam>
    /// <typeparam name="TResult">The destination object type.</typeparam>
    public void NewMapper<TObject, TResult>(IObjectMapper<TObject, TResult> mapper)
    {

        var key = new Tuple<Type, Type>(
            typeof(TObject),
            typeof(TResult)
        );
        
        Console.WriteLine("SET VALUE =====");
        
        _factory[key] = mapper;
        
        
        Console.WriteLine("Size V = " + _factory.Count);
    }

    /// <summary>
    /// Retrieves an object mapper for a specified source and destination type.
    /// </summary>
    /// <typeparam name="TObject">The source object type.</typeparam>
    /// <typeparam name="TResult">The destination object type.</typeparam>
    /// <returns>An object mapper that can convert from TObject to TResult.</returns>
    public IObjectMapper<TObject, TResult> Get<TObject, TResult>()
    {

        var tuple = new Tuple<Type, Type>(
            typeof(TObject), typeof(TResult)); 
        
        Console.WriteLine("GET = ");
        
        Console.WriteLine("Size = " + _factory.Count);
        
        foreach (var factoryKey in _factory.Keys)
        {
            Console.WriteLine("Key 1 : " + factoryKey.Item1.Name);
            Console.WriteLine("Key 2 : " + factoryKey.Item2.Name);  
        }
        
        return (IObjectMapper<TObject, TResult>) _factory[tuple];
    }
    
}