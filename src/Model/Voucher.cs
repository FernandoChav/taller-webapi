using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Taller1.Model;

public class Voucher
{

    [Key]
    public int Id { get; set; } = 0;
    public DateTime Date { get; set; }
    public List<VoucherProduct> AllProducts { get; set; }

    public int UserId { get; set; } = 0;
    public User User { get; set; }
    
    private int _totalPrice =  0;
    
    public int GetTotalPrice()
    {
        if (_totalPrice != 0)
        {
            return _totalPrice;
        }
        
        var total = 0;
        foreach (var product in AllProducts)
        {
            total += product.Total();
        }

        _totalPrice = total;
        return total;
    }
    
}

public class CreationVoucher
{
    
}