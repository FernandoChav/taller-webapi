namespace Taller1.Model;

public class Voucher
{

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<VoucherProduct> AllProducts { get; set; } = null!;

    public int UserId { get; set; } = 0;
    public User User { get; set; } = null!;
    
    /*private int _totalPrice =  0;
    
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
    }*/
    
}

public class CreationVoucher
{
 
    public DateTime Date { get; set; }
    public ICollection<VoucherProductCreation> Products { get; set; }
    public int UserId { get; set; }
    
}

public class VoucherResponse
{
    
    public DateTime CreatedVoucherDate { get; set; }
    public ICollection<VoucherProductResponse> Products { get; set; }
    
}


public class VoucherEdit
{
    
    public DateTime Date { get; set; }
    
}