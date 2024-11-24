using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Mapper;

/// <summary>
/// Mapper class to convert a <see cref="CreationProduct"/> object into a <see cref="Product"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToProductMapper : IObjectMapper<CreationProduct,
Product>
{


    private readonly Product _emptyProduct = new Product
    {
        AbsoluteUri = "",
        IdImage = ""
    };
    
    /// <summary>
    /// Converts a <see cref="CreationProduct"/> object into a <see cref="Product"/> object.
    /// </summary>
    /// <param name="element">The input object of type <see cref="CreationProduct"/> to be mapped.</param>
    /// <param name="parameters">
    /// An optional set of additional parameters of type <see cref="ObjectParameters"/>.
    /// This may include values like "AbsoluteUri" and "ImageId" used during the mapping.
    /// </param>
    /// <returns>
    /// A <see cref="Product"/> object populated with data from the input object and additional parameters.
    /// If no parameters are provided, an empty <see cref="Product"/> object is returned.
    /// </returns>
    public Product Mapper(CreationProduct element, ObjectParameters? parameters)
    {
        if (parameters == null)
        {
            return _emptyProduct;
        }

        var absoluteUri = parameters.GetString("AbsoluteUri");
        var idImage = parameters.GetString("ImageId");        


        return new Product
        {
            Name = element.EName,
            Price = element.Price,
            ProductType = element.ProductType,
            Stock = element.Stock,
            AbsoluteUri = absoluteUri,
            IdImage = idImage
        };

    }
    
}