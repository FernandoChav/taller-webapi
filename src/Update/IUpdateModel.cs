using Taller1.Util;

namespace Taller1.Update;

/// <summary>
/// This interface perms update a model from a parameters
/// </summary>
/// <typeparam name="TModelObject"></typeparam>

public interface IUpdateModel<TModelObject>
{

    /// <summary>
    /// Edit a model from parameters
    /// </summary>
    /// <param name="parameters">A set parameters for update</param>
    /// <param name="modelObject">The model for updating</param>
    
    void Edit(ObjectParameters parameters,
        TModelObject modelObject);

}
