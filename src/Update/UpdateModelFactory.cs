namespace Taller1.Update;

public class UpdateModelFactory
{

    private readonly IDictionary<Tuple<Type, Type>, object> _factory = new Dictionary<
        Tuple<Type, Type>, object>();

    public IUpdateModel<TEditObject, TModelObject> Provide<TEditObject, TModelObject>()
    {
        return (IUpdateModel<TEditObject, TModelObject>) _factory[
            new Tuple<Type, Type>(
                typeof(TEditObject), 
                typeof(TModelObject))
        ];
    }

    public void Add<TEditObject, TModelObject>(IUpdateModel<TEditObject, TModelObject> updateModel)
    {

        var tuple = new Tuple<Type,
            Type>(
            typeof(TEditObject),
            typeof(TModelObject)
        );

        _factory[tuple] = updateModel;
    }
    

}