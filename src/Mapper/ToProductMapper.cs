using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToProductMapper : IObjectMapper<CreationProduct,
Product>
{

    private readonly Product _emptyProduct = new Product
    {
        AbsoluteUri = "",
        IdImage = ""
    };
    
    public Product Mapper(CreationProduct element, ObjectParameters? parameters)
    {
        if (parameters == null)
        {
            return _emptyProduct;
        }

        var absoluteUri = parameters.GetString("absoluteUri");
        var idImage = parameters.GetString("idImage");        


        return new Product
        {
            Name = element.Name,
            Price = element.Price,
            ProductType = element.ProductType,
            Stock = element.Stock,
            AbsoluteUri = absoluteUri,
            IdImage = idImage
        };

    }
    
}