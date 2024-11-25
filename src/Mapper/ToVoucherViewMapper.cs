using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;
/// <summary>
/// Mapper class to transform a <see cref="Voucher"/> object into a <see cref="VoucherView"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToVoucherViewMapper : IObjectMapper<Voucher,
VoucherView>
{
    
    /// <summary>
    /// Maps a <see cref="Voucher"/> object to a <see cref="VoucherView"/> object.
    /// </summary>
    /// <param name="element">The <see cref="Voucher"/> object containing the voucher's data.</param>
    /// <param name="parameters">
    /// Optional <see cref="ObjectParameters"/> for additional mapping context (not used in this implementation).
    /// </param>
    /// <returns>
    /// A <see cref="VoucherView"/> object representing the simplified view of the voucher for presentation.
    /// </returns>
    public VoucherView Mapper(Voucher element, ObjectParameters? parameters)
    {
        
        var products = new List<ItemVoucherView>();
        foreach (var product in element.AllProducts)
        {
            products.Add(
                new ItemVoucherView 
                {
                    Name = product.Name,
                    Type = product.Type,
                    Elements = product.Elements,
                    Price = product.Price
                }
            );
        }

        
        return new VoucherView
        {
            CreatedVoucherDate = element.Date,
            Products = products
        };
    }
    
}