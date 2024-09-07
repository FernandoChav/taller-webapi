using System.Globalization;

namespace Taller1.src.Service
{
    public interface IObjectService<TEntity>
    {

        void Push(TEntity entity);

        void Delete(String id);

     
    }

}
