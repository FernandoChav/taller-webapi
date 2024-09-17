using Taller1.src.Models;

namespace Taller1.Mapper;

public class CreationProductObjectMapper : IObjectMapper<CreationProduct, Product>
{
    
    public CreationProduct Mapper(Product entity)
    {
        return new CreationProduct
        {
            ImageUrl = entity.ImageUrl,
            Name = entity.Name,
            Price = entity.Price,
            ProductType = entity.ProductType,
            Stock = entity.Stock
        };
    }

    public Product Mapper(CreationProduct entity)
    {
        return new Product()
        {
            Id = 0,
            ImageUrl = entity.ImageUrl,
            Price = entity.Price,
            ProductType = entity.ProductType,
            Name = entity.Name,
            Stock = entity.Stock
        };
    }

}