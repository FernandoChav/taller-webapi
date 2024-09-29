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

        private readonly IObjectMapper<CreationProduct, Product> _productCreationDtoMapper
            = new CreationProductObjectMapper();

        /// <summary>
        /// Store a product based a new request product
        /// </summary>
        /// <param name="creationProduct">A product for create</param>
        /// <returns>A wrapper from product created</returns>

        [HttpPost]
        [Route("/create")]
        public ActionResult<Product> Post([FromBody] CreationProduct creationProduct)
        {
            var product = _productCreationDtoMapper.Mapper(creationProduct);

            service.Push(product);
            return product;
        }

        /// <summary>
        /// Delete a product from her string id
        /// </summary>
        /// <param name="id">a string id product</param>

        [HttpDelete]
        [Route("/delete/{id}")]
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
        [Route("/find/{id}")]
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
        /// <returns>A wrapper that contains a set elements product</returns>

        [HttpGet]
        [Route("/all-available")]
        public ActionResult<EntityGroup<Product>> All(
            [FromQuery] int page,
            [FromQuery] int elements
        )
        {
            return EntityGroup<Product>
                .Create(
                    DbSetSearchBuilder<Product>.NewBuilder(
                            _products
                        ).Page(page, elements)
                        .Filter(product => product.StockAvailable())
                        .BuildAndGetAll(),
                    new Dictionary<string, string>
                    {
                        ["Page"] = page.ToString(),
                        ["Elements"] = elements.ToString()
                    }
                );
        }
    }
}