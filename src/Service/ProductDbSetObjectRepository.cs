using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;

namespace Taller1.Service
{
    public class ProductDbSetObjectRepository : IObjectRepository<Product>
    {
        private readonly DbSet<Product> _products;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductDbSetObjectRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _products = applicationDbContext.Products;
        }

        public void Delete(int id)
        {
            var product = FindById(id);
            _products.Remove(product);
            _applicationDbContext.SaveChanges();
        }

        public void Push(Product entity)
        {
            _products.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public Product FindById(int id)
        {
            return _products
                .SingleOrDefault(
                    product => product.Id == id
                );
        }
    }
}