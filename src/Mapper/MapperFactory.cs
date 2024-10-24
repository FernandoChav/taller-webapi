namespace Taller1.Mapper;

public class MapperFactory : IMapperFactory
{

    private IDictionary<Tuple<Type, Type>, object> _factory;

    public MapperFactory()
    {
        _factory = new Dictionary<Tuple<Type, Type>, object>();
    }

    public void NewMapper(Type typeObject, Type typeResult, IObjectMapper<Type, Type> mapper)
    {

        var tuple = new Tuple<Type, Type>
            (typeObject, typeResult);
        
        _factory[tuple] = mapper;

    }

    public IObjectMapper<TObject, TResult> Get<TObject, TResult>()
    {

        var tuple = new Tuple<Type, Type>(
            typeof(TObject), typeof(TResult)); 
        
        return (IObjectMapper<TObject, TResult>) _factory[tuple];
    }
    
}