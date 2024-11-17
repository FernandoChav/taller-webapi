using Taller1.Util;

namespace Taller1.Mapper;

/// <summary>
/// This interface provide a way for transform a object to other.
/// Let x,y two object any
///
/// This class provider a transformation for x and y and 
/// </summary>
/// <typeparam name="TObject">Object type one</typeparam>
/// <typeparam name="TResult">Object type two</typeparam>
public interface IObjectMapper<TObject, TResult>
{
    /// <summary>
    /// Transform object TE2 to object TE1
    /// </summary>
    /// <param name="element">Object any</param>
    /// <param name="parameters">Other parameters for construcction element</param>
    /// <returns>A Object transformed</returns>
    TResult Mapper(TObject element, ObjectParameters? parameters);

    /// <summary>
    /// Transform a list of elements TObject to others
    /// </summary>
    /// <param name="elements">A List Object</param>
    /// <param name="parameters">Other parameters for construcction element</param>
    /// <returns>A List of object returned</returns>
    IList<TResult> Mapper(IList<TObject> elements,
        ObjectParameters? parameters)
    {
        var elementsMapped = new List<TResult>();
        foreach (var element in elements)
        {
            elementsMapped.Add(Mapper(element, parameters));
        }

        return elementsMapped;
    }

    /// <summary>
    /// Transform object TE2 to object TE1
    /// </summary>
    /// <param name="element">Object any</param>
    /// <returns>A Object transformed</returns>

    TResult Mapper(TObject element)
    {
        return Mapper(element,
            null);
    }
    
    /// <summary>
    /// Transform a list of elements TObject to others
    /// </summary>
    /// <param name="elements">A List Object</param>
    /// <returns>A List of object returned</returns>

    IList<TResult> Mapper(IList<TObject> elements)
    {
        return Mapper(elements, null);
    }
    
}