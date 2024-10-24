using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.src.Models;
using Taller1.TException;

namespace Taller1.Service;

public class VoucherRepository : IObjectRepository<Voucher, VoucherEdit>
{

    private readonly DbSet<Voucher> _vouchers;
    private readonly DbSet<VoucherProduct> _voucherProducts;
    private readonly ApplicationDbContext _applicationDbContext;

    public VoucherRepository(ApplicationDbContext applicationDbContext)
    {
        _vouchers = applicationDbContext.Vouchers;
        _voucherProducts = applicationDbContext.VoucherProducts;
        _applicationDbContext = applicationDbContext;
    }
    
    public void Push(Voucher entity)
    {
        _vouchers.Add(entity);
        
        foreach (var product in entity.AllProducts)
        {
            product.VoucherId = entity.Id;
        }
        
        _applicationDbContext.SaveChanges();
    }

    public Voucher Delete(int id)
    {
        var voucher = FindById(id);
        if (voucher == null)
        {
            throw new ElementNotFound();
        }

        _vouchers.Remove(voucher);
        _applicationDbContext.SaveChanges();
        return voucher;
    }

    public Voucher? FindById(int id)
    {
        return _vouchers
            .Include(v => v.AllProducts)
            .FirstOrDefault(v => v.Id == id);
    }

    public void Edit(int id, VoucherEdit entityEdit)
    {
        throw new NotImplementedException();
    }
    
}