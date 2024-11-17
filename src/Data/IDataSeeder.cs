using Microsoft.EntityFrameworkCore;

namespace Taller1.Data
{
    
    /// <summary>
    /// This interface represent a way for seeder a model
    /// </summary>
    /// <typeparam name="O"></typeparam>
    
    public interface IDataSeeder<O> where O : class
    {
        
        /// <summary>
        /// Set a model to database 
        /// </summary>
        
        void Seed();

        /// <summary>
        /// Get database for seed
        /// </summary>
        /// <returns></returns>
        
        DbSet<O> Get();

    }
    
}