namespace Taller1.Model;

/// <summary>
/// Model class that represent a voucher
/// </summary>

public class Voucher
{

    /// <summary>
    /// Unique entity Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Date when is created the voucher
    /// </summary>
    
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Collection elements bought 
    /// </summary>
    
    public ICollection<ItemVoucher> AllProducts { get; set; } = null!;
    
    /// <summary>
    /// User id owner voucher
    /// </summary>
    
    public int UserId { get; set; } = 0;
    
    /// <summary>
    /// User owner voucher
    /// </summary>
    public User User { get; set; } = null!;
    
}

/// <summary>
/// Model voucher when is required store a new voucher
/// </summary>

public class VoucherCreation
{
 
    /// <summary>
    /// Date when is created the voucher
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Date when is created the voucher
    /// </summary>
    
    public ICollection<ItemVoucherCreation> Products { get; set; }
    
    /// <summary>
    /// User owner voucher
    /// </summary>
    public int UserId { get; set; }
    
}

/// <summary>
/// Voucher entity for send 
/// </summary>

public class VoucherView
{
    
    /// <summary>
    /// Date when is created the voucher
    /// </summary>
    public DateTime CreatedVoucherDate { get; set; }
    
    /// <summary>
    /// Date when is created the voucher
    /// </summary>
    
    public ICollection<ItemVoucherView> Products { get; set; }
    
}

