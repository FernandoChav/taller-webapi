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
    /// <returns>A object T2</returns>
    
    TResult Mapper(TObject element, ObjectParameters? parameters);

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
    
    TResult Mapper(TObject element)
    {
        return Mapper(element,
            null);
    }

    IList<TResult> Mapper(IList<TObject> elements)
    {
        return Mapper(elements, null);
    }
    
}