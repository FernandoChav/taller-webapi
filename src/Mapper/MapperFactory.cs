namespace Taller1.Mapper;

public class MapperFactory : IMapperFactory
{

    private readonly IDictionary<Tuple<Type, Type>, object> _factory;

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