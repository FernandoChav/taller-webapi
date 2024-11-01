using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;

namespace Taller1.Service
{
    public class ProductRepository : IObjectRepository<Product>
    {
        private readonly DbSet<Product> _products;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUpdateModel<Product> _updateModel;
        private readonly IImageService _imageService;

        public ProductRepository(ApplicationDbContext applicationDbContext,
            IUpdateModel<Product> updateModel,
            IImageService imageService)
        {
            _applicationDbContext = applicationDbContext;
            _products = applicationDbContext.Products;
            _updateModel = updateModel;
            _imageService = imageService;
        }

        public async Task<Product> PushAsync(Product entity)
        {
            await _products.AddAsync(entity);
            return entity;
        }

        public void Push(Product entity)
        {
            _products.Add(entity);
            _applicationDbContext.SaveChanges();
        }


        public Product? Delete(int id)
        {
            var product = FindById(id);
            if (product == null)
            {
                return null;
            }

            _imageService.Destroy(product.IdImage);
            _products.Remove(product);
            _applicationDbContext.SaveChanges();
            return product;
        }

        public Product? FindById(int id)
        {
            return _products
                .SingleOrDefault(
                    product => product.Id == id
                );
        }

        public Task<Product?> FindByIdAsync(int id)
        {
            return _products.SingleOrDefaultAsync(
                product => product.Id == id
            );
        }

        public Product? Edit(int id, ObjectParameters parameters)
        {
            var product = FindById(id);
            if (product == null)
            {
                return null;
            }

            _updateModel.Edit(parameters, product);
            _applicationDbContext.SaveChanges();

            return product;
        }
    }
}