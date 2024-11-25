using Microsoft.EntityFrameworkCore;
using Taller1.Data;
using Taller1.Model;
using Taller1.src.Models;
using Taller1.TException;
using Taller1.Util;

namespace Taller1.Service;

/// <summary>
/// Repository class for managing Voucher entities.
/// Provides methods for CRUD operations on Voucher.
/// </summary>
public class VoucherRepository : IObjectRepository<Voucher>
{
    private readonly DbSet<Voucher> _vouchers;
    private readonly ApplicationDbContext _applicationDbContext;

    /// <summary>
    /// Initializes a new instance of the VoucherRepository.
    /// </summary>
    /// <param name="applicationDbContext">The database context for interacting with the database.</param>
    public VoucherRepository(ApplicationDbContext applicationDbContext)
    {
        _vouchers = applicationDbContext.Vouchers;
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// Adds a new voucher to the database synchronously.
    /// Also assigns the VoucherId to the related products.
    /// </summary>
    /// <param name="voucher">The voucher entity to be added.</param>
    public void Push(Voucher voucher)
    {
        _vouchers.Add(voucher);

        foreach (var product in voucher.AllProducts)
        {
            product.VoucherId = voucher.Id;
        }

        _applicationDbContext.SaveChanges();
    }
    /// <summary>
    /// Adds a new voucher to the database asynchronously.
    /// Also assigns the VoucherId to the related products.
    /// </summary>
    /// <param name="voucher">The voucher entity to be added.</param>
    /// <returns>The added voucher entity.</returns>
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
    /// <summary>
    /// Deletes a voucher by its ID synchronously.
    /// </summary>
    /// <param name="id">The ID of the voucher to be deleted.</param>
    /// <returns>The deleted voucher entity, or null if not found.</returns>
    public Voucher? Delete(int id)
    {
        var voucher = FindById(id);
        if (voucher == null)
        {
            return null;
        }

        _vouchers.Remove(voucher);
        _applicationDbContext.SaveChanges();
        return voucher;
    }
    /// <summary>
    /// Deletes a voucher by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the voucher to be deleted.</param>
    /// <returns>The deleted voucher entity, or null if not found.</returns>
    public async Task<Voucher?> DeleteAsync(int id)
    {
        var voucher = await FindByIdAsync(id);
        if (voucher == null)
        {
            return null;
        }

        _vouchers.Remove(voucher);
        await _applicationDbContext.SaveChangesAsync();
        
        return voucher;
    }

    /// <summary>
    /// Finds a voucher by its ID synchronously.
    /// Includes the related products.
    /// </summary>
    /// <param name="id">The ID of the voucher to be found.</param>
    /// <returns>The found voucher entity, or null if not found.</returns>
    public Voucher? FindById(int id)
    {
        return _vouchers
            .Include(v => v.AllProducts)
            .FirstOrDefault(v => v.Id == id);
    }
    /// <summary>
    /// Finds a voucher by its ID asynchronously.
    /// Includes the related products.
    /// </summary>
    /// <param name="id">The ID of the voucher to be found.</param>
    /// <returns>A task that represents the asynchronous operation, with the voucher entity or null if not found.</returns>
    public Task<Voucher?> FindByIdAsync(int id)
    {
        return _vouchers
            .Include(v => v.AllProducts)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
    /// <summary>
    /// Edits an existing voucher by its ID using the provided parameters. This method is not implemented.
    /// </summary>
    /// <param name="id">The ID of the voucher to be edited.</param>
    /// <param name="parameters">The parameters for editing the voucher.</param>
    /// <returns>Throws NotImplementedException as editing is not supported.</returns>
    public Voucher? Edit(int id, ObjectParameters parameters)
    {
        throw new NotImplementedException();
    }
}