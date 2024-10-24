﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Search;
using Taller1.Service;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Util;

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
        IObjectRepository<Product, ProductEdit> service,
        ImageService imageService,
        ApplicationDbContext applicationDbContext,
        MapperFactory mapperFactory)
        : ControllerBase
    {
        private readonly DbSet<Product> _products = applicationDbContext.Products;

        private readonly IObjectMapper<CreationProduct, Product> _creationProductMapper = mapperFactory
            .Get<CreationProduct, Product>();

        private readonly IObjectMapper<Product, ProductView> _productViewMapper =
            mapperFactory.Get<Product, ProductView>();

        /// <summary>
        /// Store a product based a new request product
        /// </summary>
        /// <param name="creationProduct">A product for create</param>
        /// <returns>A wrapper from product created</returns>
        [HttpPost]
        [Route("/product/create")]
        public ActionResult<Product> Post([FromBody] CreationProduct creationProduct)
        {
            var task = imageService.Upload(creationProduct.Image);
            var result = task.Result;

            var publicId = result.PublicId;
            var absoluteUri = result.SecureUrl.AbsoluteUri;

            var product
                = _creationProductMapper.Mapper(creationProduct,
                    ObjectParameters.Create()
                        .AddParameter("ImageId", publicId)
                        .AddParameter("AbsoluteUri", absoluteUri));

            service.Push(product);
            return Ok(product);
        }

        /// <summary>
        /// Delete a product from her string id
        /// </summary>
        /// <param name="id">a string id product</param>
        [HttpDelete]
        [Route("/product/delete/{id}")]
        public ActionResult<ProductView> Delete(
            int id)
        {
            var productDeleted = service.Delete(id);
            if (productDeleted == null)
            {
                return NotFound("Product not found"); 
            }

            return _productViewMapper.
                Mapper(productDeleted);
        }

        [HttpPut]
        [Route("/product/update/{id}")]
        public ActionResult<ProductView> Update(
            int id,
            [FromBody] ProductEdit productEdit
        )
        {
            var product = service.Edit(id, productEdit);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            
            return _productViewMapper.Mapper(product);
        }


        /// <summary>
        /// Find a product from her id 
        /// </summary>
        /// <param name="id">a string id product</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/product/find/{id}")]
        public ActionResult<ProductView> Find(
            int id)

        {
            var product = service.FindById(id);
            if (product == null)
            {
                return NotFound("Element not found");
            }

            return _productViewMapper.Mapper(product);
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
        [Route("/product/all")]
        public ActionResult<EntityGroup<ProductView>> All(
            [FromQuery] int page = 1,
            [FromQuery] int elements = 10,
            [FromQuery] bool isOrderingByPrice = false,
            [FromQuery] bool ascending = false,
            [FromQuery] string filteringByNameProduct = ""
        )
        {
            var builder =
                DbSetSearchBuilder<Product>.NewBuilder(
                        _products
                    ).Page(page, elements)
                    .Filter(product => product.StockAvailable());

            if (filteringByNameProduct != Constants.EmptyString)
            {
                builder = builder.Filter(product => product.Name == filteringByNameProduct);
            }

            if (isOrderingByPrice)
            {
                builder = builder.OrderBy(product => product.Price, ascending);
            }

            var elementsShowed = _productViewMapper.
                Mapper(builder.BuildAndGetAll());

            return EntityGroup<ProductView>.Create(
                elementsShowed,
                new Dictionary<string, string>
                {
                    ["Page"] = page.ToString(),
                    ["Elements"] = elements.ToString()
                }
            );
        }
    }
}