namespace Taller1.Model;

/// <summary>
/// This class represent a collection of elements for retrieve
/// </summary>
/// <typeparam name="T">The type object</typeparam>

public class EntityGroup<T>
{
    
    /// <summary>
    /// This is a collection that hold elements
    /// </summary>
    
    public required IEnumerable<T> Entities { get; set; }
    
    /// <summary>
    /// A dictionary that contains a set of properties about the
    /// elements retrieve, example, numbers elements, etc. 
    /// </summary>
    public required Dictionary<string, string> Metadata { get; set; }

    /// <summary>
    /// </summary>
    /// A way for create instance from EntityGroup
    /// <param name="entities">This is a collection that hold elements/param>
    /// <param name="metadata">Properties about of collection</param>
    /// <returns>A new instance EntityGroup</returns>
    
    public static EntityGroup<T> Create(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        return new EntityGroup<T>
        {
            Entities = entities,
            Metadata = metadata
        };
    }
    
}