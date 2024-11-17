using Taller1.Model;
using Taller1.Util;

namespace Taller1.Mapper;

public class ToVoucherViewMapper : IObjectMapper<Voucher,
VoucherView>
{
    
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