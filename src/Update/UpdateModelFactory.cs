namespace Taller1.Update;

public class UpdateModelFactory
{

    private readonly IDictionary<Type, object> _factory = new Dictionary<
        Type, object>();

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