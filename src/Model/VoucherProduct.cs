using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Taller1.src.Models;

namespace Taller1.Model;


public class VoucherProduct
{

    public int Id { get; set; } = 0;
    public string Name { get; set; }
    
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public int Elements { get; set; }

    public int VoucherId { get; set; } 
    public required Voucher Voucher { get; set; }
    
    public int Total()
    {
        return Price * Elements;
    }
    
}