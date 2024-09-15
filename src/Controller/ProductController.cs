﻿using Microsoft.AspNetCore.Http;
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
        [Route("/create/")]
        public ActionResult<Product> Post(Product product)
        {
            _service.Push(product);
            return product;
        }
    }

}
