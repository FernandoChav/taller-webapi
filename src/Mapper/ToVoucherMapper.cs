using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;
/// <summary>
/// Mapper class to transform a <see cref="VoucherCreation"/> object into a <see cref="Voucher"/> object.
/// Implements the <see cref="IObjectMapper{TObject, TResult}"/> interface.
/// </summary>
public class ToVoucherMapper : IObjectMapper<VoucherCreation,
Voucher>
{


    /// <summary>
    /// Maps a <see cref="VoucherCreation"/> object to a <see cref="Voucher"/> object.
    /// </summary>
    /// <param name="element">The <see cref="VoucherCreation"/> object containing voucher creation data.</param>
    /// <param name="parameters">
    /// Optional <see cref="ObjectParameters"/> for additional mapping context (not used in this implementation).
    /// </param>
    /// <returns>
    /// A <see cref="Voucher"/> object representing a complete voucher, including its associated products.
    /// </returns>
    public Voucher Mapper(VoucherCreation element,
        ObjectParameters? parameters)
    {

        var products = new List<ItemVoucher>();
        
        foreach (var productCreation in element.Products)
        {
            var voucherProduct = new ItemVoucher 
            {
                Name = productCreation.Name,
                Type = productCreation.Type,
                Price = productCreation.Price,
                Elements = productCreation.Elements,
            };

            products.Add(voucherProduct);
        }

        return new Voucher
        {
            Date = element.Date,
            UserId = element.UserId,
            AllProducts = products
        };
        
    }
    
}