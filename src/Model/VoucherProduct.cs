using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Taller1.src.Models;

namespace Taller1.Model;


public class VoucherProduct
{
    
    public int Id { get; set; }
    
    [MaxLength(120)]
    public required string Name { get; set; }
    public required ProductType Type { get; set; }
    public required int Price { get; set; }
    public required int Elements { get; set; }

    public int VoucherId { get; set; } 
    public Voucher Voucher { get; set; }
    
}

public class VoucherProductCreation
{
    
    [MaxLength(120)]
    public required string Name { get; set; }
    public required ProductType Type { get; set; }
    public required int Price { get; set; }
    public required int Elements { get; set; }
    
}

public class VoucherProductResponse
{
    
    [MaxLength(120)]
    public required string Name { get; set; }
    public required ProductType Type { get; set; }
    public required int Price { get; set; }
    public required int Elements { get; set; }
    
}