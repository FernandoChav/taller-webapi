using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Search;
using Taller1.Service;
using Taller1.src.Models;

namespace Taller1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IObjectService<Product> _service;
        private readonly ImageService _imageService;
        private readonly DbSet<Product> _products;

        public ProductController(IObjectService<Product> service, ImageService imageService,
            AplicationDbContext aplicationDbContext)
        {
            _service = service;
            _imageService = imageService;
            _products = aplicationDbContext.Products;
        }

        [HttpPost]
        [Route("/create")] 
        public ActionResult<Product> Post([FromBody] Product product)
        {
            _service.Push(product);
            return product;
        }

        [HttpGet]
        [Route("/all")]
        public ActionResult<IEnumerable<Product>> All(
                [FromQuery] int page,
            [FromQuery] int elements
            )
        {
            return DbSetSearchBuilder<Product>.NewBuilder(
                    _products
                ).Page(page, elements)
                .BuildAndGetAll();
        }

    }
}