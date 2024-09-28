namespace Taller1.Model;

public class EntityGroup<T>
{

    private readonly IEnumerable<T> _entities;
    private readonly Dictionary<string, string> _metadata;

    public EntityGroup(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        _entities = entities;
        _metadata = metadata;
    }

    public IEnumerable<T> GetEntities()
    {
        return _entities;
    }

    public Dictionary<string, string> GetMetadata()
    {
        return _metadata;
    }

    public static EntityGroup<T> Create(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        return new EntityGroup<T>(entities, metadata);
    }
    
}