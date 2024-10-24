using Microsoft.EntityFrameworkCore;

namespace Taller1.Data
{
    public interface IDataSeeder<O> where O : class
    {
        void Seed();

        DbSet<O> Get();

    }
    
}