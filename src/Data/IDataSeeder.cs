using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Taller1.Data
{
    public interface IDataSeeder<O> where O : class
    {
        void Seed();

        DbSet<O> Get();

    }
    
}