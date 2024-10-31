namespace Taller1.Mapper;

public interface IMapperFactory
{

    void NewMapper<TObject, TResult>(
        IObjectMapper<TObject, TResult> mapper);

    IObjectMapper<TObject, TResult> Get<TObject, TResult>();

}