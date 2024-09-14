using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;

namespace Taller1.Service
{
    public class ProductDbSetObjectService : IObjectService<Product>
    {

        private readonly DbSet<Product> _products;
        public ProductDbSetObjectService(AplicationDbContext aplicationDbContext)
        {
            this._products = aplicationDbContext.Products;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Push(Product entity)
        {
            _products.Add(entity);
        }

        public Product FindById(int id)
        {
            return _products
                .Where(p => p.Id == id)
                .First();
        }


    }
}
