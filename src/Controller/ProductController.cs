using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.src.Models;

namespace Taller1.Controller
{

    /// <summary>
    /// This is a class that deliver a set method for handle products
    /// 
    /// This is the main constructor 
    /// </summary>
    /// <param name="service">A repository with product</param>
    /// <param name="imageService">A image handler </param>
    /// <param name="applicationDbContext">A database manager</param>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ProductController(
        IObjectRepository<Product> service,
        ImageService imageService,
        ApplicationDbContext applicationDbContext)
        : ControllerBase
    {
        private readonly ImageService _imageService = imageService;
        private readonly DbSet<Product> _products = applicationDbContext.Products;

        /// <summary>
        /// Store a product based a new request product
        /// </summary>
        /// <param name="creationProduct">A product for create</param>
        /// <returns>A wrapper from product created</returns>

        [HttpPost]
        [Route("/product/create")]
        public ActionResult<Product> Post([FromBody] CreationProduct creationProduct)
        {

            var task = _imageService.Upload(creationProduct.Image);
            var result = task.Result;

            var publicId = result.PublicId;
            var absoluteUri = result.SecureUrl.AbsoluteUri;
            
            var productCreationDtoMapper
                = new CreationProductObjectMapper(
                    publicId,
                    absoluteUri);
            
            var product = productCreationDtoMapper.Mapper(creationProduct);

            service.Push(product);
            return product;
        }

        /// <summary>
        /// Delete a product from her string id
        /// </summary>
        /// <param name="id">a string id product</param>

        [HttpDelete]
        [Route("/product/delete/{id}")]
        public void Delete(
            [FromQuery] int id)
        {
            service.Delete(id);
        }

        /// <summary>
        /// Find a product from her id 
        /// </summary>
        /// <param name="id">a string id product</param>
        /// <returns></returns>

        [HttpGet]
        [Route("/product/find/{id}")]
        public ActionResult<Product> Find(
            [FromQuery] int id)

        {
            return service.FindById(id);
        }

        /// <summary>
        /// Find a set products from a page
        /// </summary>
        /// <param name="page">number page to search</param>
        /// <param name="elements">number elements for show</param>
        /// <param name="isOrderingByPrice">number elements for show</param>
        /// <param name="ascending">number elements for show</param>
        /// <param name="filteringByNameProduct">number elements for show</param>        
        /// <returns>A wrapper that contains a set elements product</returns>

        [HttpGet]
        [Route("/product/all-available")]
        public ActionResult<EntityGroup<Product>> All(
            [FromQuery] int page,
            [FromQuery] int elements,
            [FromQuery] bool isOrderingByPrice,
            [FromQuery] bool ascending,
            [FromQuery] string filteringByNameProduct
        )
        {
            var builder = 
                    DbSetSearchBuilder<Product>.NewBuilder(
                            _products
                        ).Page(page, elements)
                        .Filter(product => product.StockAvailable());

            if (filteringByNameProduct != "")
            {
                builder = builder.Filter(product => product.Name == filteringByNameProduct);
            }
            
            if (isOrderingByPrice)
            {
                builder = builder.OrderBy(product => product.Price, ascending);
            }
            
            return EntityGroup<Product>.Create(
                builder.BuildAndGetAll(),
                new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                }
            );
        }
    }
}