namespace Taller1.Update;

public interface IUpdateModel<TEditObject,
    TModelObject>
{

    void Edit(TEditObject editObject,
        TModelObject modelObject);

}