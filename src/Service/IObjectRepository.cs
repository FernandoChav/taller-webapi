
namespace Taller1.Service
{
    
    /// <summary>
    /// This interface represent a repository for access
    /// a content data, for example: cache, database
    /// </summary>
    /// <typeparam name="TEntity">This is a Type Object for handle</typeparam>
    
    public interface IObjectRepository<TEntity,
        TEntityEdit>
    {

        /// <summary>
        /// Store element in a repository
        /// </summary>
        /// <param name="entity">Element to save</param>
        
        void Push(TEntity entity);
        
        /// <summary>
        /// Delete element from a repository
        /// </summary>
        /// <param name="id">integer that represent her id</param>
        
        void Delete(int id);

        /// <summary>
        /// Retrieve a element from her id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>integer that represent her id</returns>
        
        TEntity? FindById(int id);

        
        
        void Edit(int id, 
            TEntityEdit entityEdit);

    }

}
