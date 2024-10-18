using Taller1.src.Models;

namespace Taller1.Mapper;

public class CreationProductObjectMapper : IObjectMapper<CreationProduct, Product>
{

    private readonly string _absoluteUri;
    private readonly string _idImage;

    public CreationProductObjectMapper(
        string absoluteUri,
        string idImage
    )
    {
        _absoluteUri = absoluteUri;
        _idImage = idImage;
    }
    
    public Product Mapper(CreationProduct creationProduct)
    {
        return new Product
        {
            Name = creationProduct.Name,
            Price = creationProduct.Price,
            ProductType = creationProduct.ProductType,
            Stock = creationProduct.Stock,
            AbsoluteUri = _absoluteUri,
            IdImage =  _idImage
        };
        
    }
    
}