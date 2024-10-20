using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;
using Taller1.Update;

namespace Taller1.Service
{
    public class ProductDbSetObjectRepository : IObjectRepository<Product, ProductEdit>
    {
        private readonly DbSet<Product> _products;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUpdateModel<ProductEdit, Product> _updateModel;

        public ProductDbSetObjectRepository(ApplicationDbContext applicationDbContext,
            IUpdateModel<ProductEdit, Product> updateModel)
        {
            _applicationDbContext = applicationDbContext;
            _products = applicationDbContext.Products;
            _updateModel = updateModel;
        }

        public void Delete(int id)
        {
            var product = FindById(id);

            if (product == null)
            {
                throw new Exception("Product not exists");
            }
            
            _products.Remove(product);
            _applicationDbContext.SaveChanges();
        }

        public void Push(Product entity)
        {
            _products.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public Product? FindById(int id)
        {
            return _products
                .SingleOrDefault(
                    product => product.Id == id
                );
        }

        public void Edit(int id, ProductEdit productEdit)
        {
            var product = FindById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            
            _updateModel.Edit(productEdit, product);
            _applicationDbContext.SaveChanges();
        }
    }
}