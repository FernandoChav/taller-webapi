namespace Taller1.Model;

/// <summary>
/// This class represent a collection of elements for present 
/// </summary>
/// <typeparam name="T">The type object</typeparam>

public class EntityGroup<T>
{
    
    private readonly IEnumerable<T> _entities;
    private readonly Dictionary<string, string> _metadata;

    /// <summary>
    /// This is main constructor for create a entity group
    /// </summary>
    /// <param name="entities">A collection of entities</param>
    /// <param name="metadata">A metadata from collection</param>
    
    public EntityGroup(
        IEnumerable<T> entities,
        Dictionary<string, string> metadata
    )
    {
        _entities = entities;
        _metadata = metadata;
    }

    /// <summary>
    /// Retrieve a collection from entities
    /// </summary>
    /// <returns>A collection from entities</returns>
    
    public IEnumerable<T> GetEntities()
    {
        return _entities;
    }
    
    /// <summary>
    /// Retrieve a dictionary that contains options and metadata
    /// in a format key-value
    /// </summary>
    /// <returns>A dictionary with metadata about the entities</returns>
    
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