using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Controller
{
    /// <summary>
    /// This is a class that delivers a set of methods for handling products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IObjectRepository<Product> _service;
        private readonly CloudinaryImageService _cloudinaryImageService;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IObjectMapper<CreationProduct, Product> _creationProductMapper;
        private readonly IObjectMapper<Product, ProductView> _productViewMapper;
        private readonly DbSet<Product> _products;

        public ProductController(
            IObjectRepository<Product> service,
            CloudinaryImageService cloudinaryImageService,
            ApplicationDbContext applicationDbContext,
            IMapperFactory mapperFactory)
        {
            _service = service;
            _cloudinaryImageService = cloudinaryImageService;
            _applicationDbContext = applicationDbContext;
            _products = applicationDbContext.Products;
            _creationProductMapper = mapperFactory.Get<CreationProduct, Product>();
            _productViewMapper = mapperFactory.Get<Product, ProductView>();
        }

        /// <summary>
        /// Store a product from a new request product.
        /// </summary>
        /// <param name="creationProduct">A product to create.</param>
        /// <param name="image">The image associated with the product.</param>
        /// <returns>A wrapper for the created product.</returns>
        [HttpPost]
        [Route("/product/create")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ProductView>> Post([FromForm] CreationProduct creationProduct,
            [FromForm] IFormFile image)
        {
            var result = await _cloudinaryImageService.Upload(image);
            var publicId = result.PublicId;
            var absoluteUri = result.SecureUrl.AbsoluteUri;

            var product = _creationProductMapper.Mapper(creationProduct,
                ObjectParameters.Create()
                    .AddParameter("ImageId", publicId)
                    .AddParameter("AbsoluteUri", absoluteUri));

            await _service.PushAsync(product);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(_productViewMapper.Mapper(product));
        }

        /// <summary>
        /// Delete a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The deleted product.</returns>
        [HttpDelete]
        [Route("/product/delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<ProductView> Delete(int id)
        {
            var productDeleted = _service.Delete(id);
            if (productDeleted == null)
            {
                return NotFound("Product not found");
            }

            return _productViewMapper.Mapper(productDeleted);
        }

        /// <summary>
        /// Update a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="parameters">The parameters to update in the product.</param>
        /// <returns>The updated product.</returns>
        [HttpPut]
        [Route("/product/update/{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<ProductView> Update(int id, [FromBody] ObjectParameters parameters)
        {
            var product = _service.Edit(id, parameters);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            return _productViewMapper.Mapper(product);
        }

        /// <summary>
        /// Find a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to find.</param>
        /// <returns>The product with the given ID.</returns>
        [HttpGet]
        [Route("/product/find/{id}")]
        [AllowAnonymous] // Accessible without authentication (optional)
        public async Task<ActionResult<ProductView>> Find(int id)
        {
            var product = await _service.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound("Element not found");
            }

            return _productViewMapper.Mapper(product);
        }

        /// <summary>
        /// Get a paginated list of products.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="elements">The number of elements per page.</param>
        /// <param name="isOrderingByPrice">Whether to order by price.</param>
        /// <param name="ascending">Whether the order is ascending.</param>
        /// <param name="filteringByNameProduct">Filter by the product's name.</param>
        /// <returns>A wrapper containing the list of products and pagination information.</returns>
        [HttpGet]
        [Route("/product/all")]
        [AllowAnonymous] // Accessible without authentication (optional)
        public async Task<ActionResult<EntityGroup<ProductView>>> All(
            [FromQuery] int page = 1,
            [FromQuery] int elements = 10,
            [FromQuery] bool isOrderingByPrice = false,
            [FromQuery] bool ascending = false,
            [FromQuery] string filteringByNameProduct = "")
        {
            var builder = AsyncDbSearchBuilder<Product>.NewBuilder(_products)
                .Page(page, elements)
                .Filter(product => product.StockAvailable());

            if (!string.IsNullOrWhiteSpace(filteringByNameProduct))
            {
                builder = builder.Filter(product => product.Name == filteringByNameProduct);
            }

            if (isOrderingByPrice)
            {
                builder = builder.OrderBy(product => product.Price, ascending);
            }

            var allElements = await builder.BuildAndGetAll();
            var elementsShowed = _productViewMapper.Mapper(allElements);

            return Ok(EntityGroup<ProductView>.Create(
                elementsShowed,
                new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                }));
        }
    }
}
