using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IObjectService<Product> service, ImageService imageService)
        {
            _service = service;
            _imageService = imageService;
        }

        [HttpPost]
        [Route("/create/")] // falta agregar la ruta de acceso del id api/create/{id}
        public ActionResult<Product> Post(Product product)
        {
            _service.Push(product);
            return product;
        }
    }
}