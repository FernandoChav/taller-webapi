namespace Taller1.Update;

/// <summary>
/// A factory class for managing and providing instances of update models for different model objects.
/// </summary>
public class UpdateModelFactory
{

    /// <summary>
    /// Provides an instance of an update model for a given model object type.
    /// </summary>
    /// <typeparam name="TModelObject">The type of the model object to retrieve the update model for.</typeparam>
    /// <returns>An instance of IUpdateModel<TModelObject> associated with the specified model object type.</returns>
    private readonly IDictionary<Type, object> _factory = new Dictionary<
        Type, object>();

    /// <summary>
    /// Adds a new update model to the factory.
    /// </summary>
    /// <typeparam name="TModelObject">The type of the model object the update model corresponds to.</typeparam>
    /// <param name="updateModel">The instance of the update model to add to the factory.</param>
    public IUpdateModel<TModelObject> Provide<TModelObject>()
    {
        return (IUpdateModel<TModelObject>) _factory[
            typeof(TModelObject)
        ];
    }

    public void Add<TModelObject>(IUpdateModel<TModelObject> updateModel)
    {
        _factory[typeof(TModelObject)] = updateModel;
    }
    

}