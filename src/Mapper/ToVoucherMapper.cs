using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToVoucherMapper : IObjectMapper<VoucherCreation,
Voucher>
{
    
    public Voucher Mapper(VoucherCreation element,
        ObjectParameters? parameters)
    {

        var products = new List<VoucherProduct>();
        
        foreach (var productCreation in element.Products)
        {
            var voucherProduct = new VoucherProduct
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