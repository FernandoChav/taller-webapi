﻿using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.src.Models;

namespace Taller1.Service
{
    public class ProductDbSetObjectService : IObjectService<Product>
    {
        private readonly DbSet<Product> _products;
        private readonly AplicationDbContext _aplicationDbContext;

        public ProductDbSetObjectService(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
            _products = aplicationDbContext.Products;
        }

        public void Delete(int id)
        {
            var product = FindById(id);
            _products.Remove(product);
            _aplicationDbContext.SaveChanges();
        }

        public void Push(Product entity)
        {
            _products.Add(entity);
            _aplicationDbContext.SaveChanges();
        }

        public Product FindById(int id)
        {
            return _products
                .SingleOrDefault(
                    product => product.Id == id
                );
        }
    }
}