using Taller1.Util;

namespace Taller1.Update;

public interface IUpdateModel<TModelObject>
{

    void Edit(ObjectParameters parameters,
        TModelObject modelObject);

}