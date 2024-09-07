using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taller1.src.Models;
using Taller1.src.Service;

namespace Taller1.src.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IObjectService<Product> _service;
        public ProductController(IObjectService<Product> _service)
        {
            this._service = _service;
        }

        [HttpPost]
        [Route("/create/")]
        public ActionResult<Product> Post(Product product)
        {
            _service.Push(product);
            return product;
        }

    }

}
