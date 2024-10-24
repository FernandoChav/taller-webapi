namespace Taller1.Mapper;

public interface IMapperFactory
{

    void NewMapper(Type typeObject,
        Type typeResult,
        IObjectMapper<Type, Type> mapper);

    IObjectMapper<TObject, TResult> Get<TObject, TResult>();

}