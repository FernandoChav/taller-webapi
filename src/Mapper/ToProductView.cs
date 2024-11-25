using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Mapper;
/// <summary>
/// Mapper class to convert a <see cref="Product"/> object into a <see cref="ProductView"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToProductView : IObjectMapper<Product,
ProductView>
{

    /// <summary>
    /// Converts a <see cref="Product"/> object into a <see cref="ProductView"/> object.
    /// </summary>
    /// <param name="element">The input object of type <see cref="Product"/> to be mapped.</param>
    /// <param name="parameters">Optional additional parameters. This implementation does not use parameters.</param>
    /// <returns>
    /// A <see cref="ProductView"/> object containing a simplified view of the product data.
    /// </returns>
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