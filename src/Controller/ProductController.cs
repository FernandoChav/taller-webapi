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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(
        IObjectService<Product> service,
        ImageService imageService,
        ApplicationDbContext applicationDbContext)
        : ControllerBase
    {
        private readonly ImageService _imageService = imageService;
        private readonly DbSet<Product> _products = applicationDbContext.Products;

        private readonly IObjectMapper<CreationProduct, Product> _productCreationDtoMapper
            = new CreationProductObjectMapper();

        [HttpPost]
        [Route("/create")]
        public ActionResult<Product> Post([FromBody]
            CreationProduct creationProduct)
        {
            var product = _productCreationDtoMapper.
                Mapper(creationProduct);
            
            service.Push(product);
            return product;
        }

        [HttpDelete]
        [Route("/delete/{id}")]
        public void Delete(
            [FromQuery] int id)
        {
            service.Delete(id);
        }

        [HttpGet]
        [Route("/find/{id}")]
        public ActionResult<Product> Find(
            [FromQuery] int id)

        {
            return service.FindById(id);
        }
        
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