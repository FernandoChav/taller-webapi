using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Service;

public class VoucherRepository : IObjectRepository<Voucher>
{

    private readonly DbSet<Voucher> _vouchers;
    private readonly ApplicationDbContext _applicationDbContext;

    public VoucherRepository(ApplicationDbContext applicationDbContext)
    {
        _vouchers = applicationDbContext.Vouchers;
        _applicationDbContext = applicationDbContext;
    }
    
    public void Push(Voucher voucher)
    {
        _vouchers.Add(voucher);
        
        foreach (var product in voucher.AllProducts)
        {
            product.VoucherId = voucher.Id;
        }
        
        _applicationDbContext.SaveChanges();
    }

    public async Task<Voucher> PushAsync(Voucher voucher)
    {
        await _vouchers.AddAsync(voucher);

        foreach (var product in voucher.AllProducts)
        {
            product.VoucherId = voucher.Id;
        }

        await _applicationDbContext.SaveChangesAsync();
        return voucher;
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

    public Task<Voucher?> FindByIdAsync(int id)
    {
        return _vouchers
            .Include(v => v.AllProducts)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Voucher? Edit(int id, ObjectParameters parameters)
    {
        throw new NotImplementedException();
    }
    
}