namespace Taller1.Model;

public class Voucher
{

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<VoucherProduct> AllProducts { get; set; } = null!;

    public int UserId { get; set; } = 0;
    public User User { get; set; } = null!;
    
}

public class VoucherCreation
{
 
    public DateTime Date { get; set; }
    public ICollection<VoucherProductCreation> Products { get; set; }
    public int UserId { get; set; }
    
}

public class VoucherView
{
    
    public DateTime CreatedVoucherDate { get; set; }
    public ICollection<VoucherProductView> Products { get; set; }
    
}


public class VoucherEdit
{
    
    public DateTime Date { get; set; }
    
}