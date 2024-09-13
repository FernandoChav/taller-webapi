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

        private readonly ImageService _imageService;
        public ProductController(IObjectService<Product> _service, ImageService imageService)
        {
            this._service = _service;

             _imageService = imageService;
        }

        [HttpPost]
        [Route("/create/")] // falta agregar la ruta de acceso del id api/create/{id}
        public ActionResult<Product> Post(Product product)
        {
            _service.Push(product);
            return product;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var imageUrl = await _imageService.UploadImageAsync(stream, file.FileName);
                    return Ok(new { Url = imageUrl });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

}
