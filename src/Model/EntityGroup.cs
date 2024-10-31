namespace Taller1.Model;

/// <summary>
/// This class represent a collection of elements for present 
/// </summary>
/// <typeparam name="T">The type object</typeparam>

public class EntityGroup<T>
{
    
    public required IEnumerable<T> Entities { get; set; }
    public required Dictionary<string, string> Metadata { get; set; }

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