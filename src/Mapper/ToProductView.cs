using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToProductView : IObjectMapper<Product,
ProductView>
{
    public ProductView Mapper(Product element, ObjectParameters? parameters)
    {
        return new ProductView
        {
            Id = element.Id,
            ImageUrl = element.AbsoluteUri,
            ProductType = element.ProductType,
            Name = element.Name,
            Stock = element.Stock,
            Price = element.Price
        };
    }
    
}