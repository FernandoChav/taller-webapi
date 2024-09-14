using System.Globalization;

namespace Taller1.Service
{
    public interface IObjectService<TEntity>
    {

        void Push(TEntity entity);

        void Delete(int id);

        TEntity FindById(int id);



    }

}
