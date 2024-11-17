namespace Taller1.Mapper;


/// <summary>
/// This is interface that hold a set from
/// mappers for convert object to other
/// 
/// </summary>

public interface IMapperFactory
{
    
    /// <summary>
    /// Register a new mapper 
    /// </summary>
    /// <param name="mapper">the interface mapper</param>
    /// <typeparam name="TObject">interface object converted</typeparam>
    /// <typeparam name="TResult">interface object to convert</typeparam>

    void NewMapper<TObject, TResult>(
        IObjectMapper<TObject, TResult> mapper);

    /// <summary>
    /// Retrieve a mapper a registered 
    /// </summary>
    /// <typeparam name="TObject">interface object converted</typeparam>
    /// <typeparam name="TResult">interface object to convert</typeparam>
    /// <returns></returns>
    
    IObjectMapper<TObject, TResult> Get<TObject, TResult>();

}