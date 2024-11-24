using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Update;
using Taller1.Util;

namespace Taller1.Service
{
    /// <summary>
    /// Repository class for managing Product entities.
    /// Provides methods for CRUD operations on Product.
    /// </summary>
    public class ProductRepository : IObjectRepository<Product>
    {
        private readonly DbSet<Product> _products;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUpdateModel<Product> _updateModel;
        private readonly IImageService _imageService;

        /// <summary>
        /// Initializes a new instance of the ProductRepository.
        /// </summary>
        /// <param name="applicationDbContext">The database context for interacting with the database.</param>
        /// <param name="updateModel">The update model for editing entities.</param>
        /// <param name="imageService">The image service for handling image operations.</param>
        public ProductRepository(ApplicationDbContext applicationDbContext,
            IUpdateModel<Product> updateModel,
            IImageService imageService)
        {
            _applicationDbContext = applicationDbContext;
            _products = applicationDbContext.Products;
            _updateModel = updateModel;
            _imageService = imageService;
        }
        
        /// <summary>
        /// Adds a new product to the database asynchronously.
        /// </summary>
        /// <param name="entity">The product entity to be added.</param>
        /// <returns>The added product entity.</returns>
        public async Task<Product> PushAsync(Product entity)
        {
            await _products.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Adds a new product to the database synchronously.
        /// </summary>
        /// <param name="entity">The product entity to be added.</param>
        public void Push(Product entity)
        {
            _products.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a product by its ID synchronously.
        /// Removes the associated image from Cloudinary and deletes the product from the database.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        /// <returns>The deleted product entity, or null if not found.</returns>
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

        /// <summary>
        /// Deletes a product by its ID asynchronously.
        /// Removes the associated image from Cloudinary and deletes the product from the database.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        /// <returns>The deleted product entity, or null if not found.</returns>
        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await FindByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            await _imageService.Destroy(product.IdImage);
            _products.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
            return product;
        }

         /// <summary>
        /// Finds a product by its ID synchronously.
        /// </summary>
        /// <param name="id">The ID of the product to be found.</param>
        /// <returns>The product entity, or null if not found.</returns>
        public Product? FindById(int id)
        {
            return _products
                .SingleOrDefault(
                    product => product.Id == id
                );
        }

        /// <summary>
        /// Finds a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the product to be found.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product entity, or null if not found.</returns>
        public Task<Product?> FindByIdAsync(int id)
        {
            return _products.SingleOrDefaultAsync(
                product => product.Id == id
            );
        }

        /// <summary>
        /// Edits a product by its ID using the specified parameters.
        /// </summary>
        /// <param name="id">The ID of the product to be edited.</param>
        /// <param name="parameters">The parameters for editing the product.</param>
        /// <returns>The edited product entity, or null if the product was not found.</returns>
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