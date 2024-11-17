using System.ComponentModel.DataAnnotations;
using Taller1.src.Models;

namespace Taller1.Model;

/// <summary>
/// A item asocciated to voucher
/// </summary>

public class ItemVoucher
{
    
    /// <summary>
    /// Id voucher
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name product 
    /// </summary>
    
    [MaxLength(120)]
    public required string Name { get; set; }
    
    /// <summary>
    /// Product Type 
    /// </summary>
    public required ProductType Type { get; set; }
    
    /// <summary>
    /// The price product
    /// </summary>
    public required int Price { get; set; }
    
    /// <summary>
    /// Quantity elements exist
    /// </summary>
    public required int Elements { get; set; }
    
    /// <summary>
    /// Id voucher asocciate
    /// </summary>
    public int VoucherId { get; set; } 
    
    /// <summary>
    /// Element voucher asocciate
    /// </summary>
    
    public Voucher Voucher { get; set; }
    
}

/// <summary>
/// Model for create a new item voucher
/// </summary>

public class ItemVoucherCreation
{
    
    /// <summary>
    /// Name product 
    /// </summary>
    
    [MaxLength(120)]
    public required string Name { get; set; }
    
    /// <summary>
    /// Product Type 
    /// </summary>
    public required ProductType Type { get; set; }
    
    /// <summary>
    /// The price product
    /// </summary>
    public required int Price { get; set; }
    
    /// <summary>
    /// Quantity elements exist
    /// </summary>
    public required int Elements { get; set; }
    
}

/// <summary>
/// Model voucher for show
/// </summary>

public class ItemVoucherView
{
    
    /// <summary>
    /// Name product 
    /// </summary>

    
    [MaxLength(120)]
    public required string Name { get; set; }
    
        
    /// <summary>
    /// Product Type 
    /// </summary>
    
    public required ProductType Type { get; set; }
    
    /// <summary>
    /// The price product
    /// </summary>
    
    public required int Price { get; set; }
    
    /// <summary>
    /// Quantity elements exist
    /// </summary>
    public required int Elements { get; set; }
    
}