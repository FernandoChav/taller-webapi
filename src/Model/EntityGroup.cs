namespace Taller1.Model;

public class EntityGroup<T>
{

    public IEnumerable<T> _entities { get; }
    public Dictionary<string, string> _metadata { get; }

    public EntityGroup(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        _entities = entities;
        _metadata = metadata;
    }

    public static EntityGroup<T> Create(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        return new EntityGroup<T>(entities, metadata);
    }
    
}