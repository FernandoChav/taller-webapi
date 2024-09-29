namespace Taller1.Mapper;

/// <summary>
/// This interface provide a way for transform a object to other.
/// Let x,y two object any
///
/// This class provider a transformation for x and y and 
/// </summary>
/// <typeparam name="TE1">Object type one</typeparam>
/// <typeparam name="TE2">Object type two</typeparam>

public interface IObjectMapper<TE1, TE2>
{
    
    /// <summary>
    /// Transform object TE2 to object TE1
    /// </summary>
    /// <param name="entity">Object any</param>
    /// <returns>A object T2</returns>
    
    TE1 Mapper(TE2 entity);

    /// <summary>
    /// Transform object TE1 to object TE2
    /// </summary>
    /// <param name="entity">Object any</param>
    /// <returns>A object T1</returns>

    
    TE2 Mapper(TE1 entity);

}