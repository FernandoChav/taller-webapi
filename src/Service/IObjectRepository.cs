
using Taller1.Util;

namespace Taller1.Service
{
    
    /// <summary>
    /// This interface represent a repository for access
    /// a content data, for example: cache, database
    /// </summary>
    /// <typeparam name="TEntity">This is a Type Object for handle</typeparam>
    
    public interface IObjectRepository<TEntity>
    {

        /// <summary>
        /// Store element in a repository
        /// </summary>
        /// <param name="entity">Element to save</param>
        
        void Push(TEntity entity);

        Task<TEntity> PushAsync(TEntity entity);
        
        /// <summary>
        /// Delete element from a repository
        /// </summary>
        /// <param name="id">integer that represent her id</param>
        
        TEntity? Delete(int id);

        /// <summary>
        /// Retrieve a element from her id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>integer that represent her id</returns>
        
        TEntity? FindById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        Task<TEntity?> FindByIdAsync(int id);

        
        
        TEntity? Edit(int id, 
            ObjectParameters parameters);

    }

}
