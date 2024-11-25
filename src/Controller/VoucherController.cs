using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Taller1.Mapper;
using Taller1.Model;
using Taller1.Service;

namespace Taller1.Controller;

[ApiController]
[Route("api/[controller]")]
public class VoucherController(
    IObjectRepository<Voucher> voucherRepository,
    IMapperFactory mapperFactory
) : ControllerBase
{
    private readonly IObjectMapper<VoucherCreation, Voucher> _toMapperVoucher = mapperFactory.Get<
        VoucherCreation, Voucher>();
    private readonly IObjectMapper<Voucher, VoucherView> _toMapperVoucherView = mapperFactory.Get<
        Voucher, VoucherView>();

    /// <summary>
    /// Create a new voucher and save
    /// </summary>
    /// <param name="creationVoucher">A voucher creating request</param>
    /// <returns>The voucher created element</returns>
    [HttpPost]
    [Route("/voucher/create")]
    public VoucherCreation Create([FromBody] VoucherCreation creationVoucher)
    {
        var voucher = _toMapperVoucher.Mapper(creationVoucher);
        voucherRepository.Push(voucher);

        return creationVoucher;
    }

    /// <summary>
    /// Find a voucher by its ID
    /// </summary>
    /// <param name="id">Voucher ID</param>
    /// <returns>The voucher found</returns>
    /// <response code="200">Returns the voucher found</response>
    /// <response code="404">If the voucher is not found</response>
    [HttpGet]
    [Route("/voucher/find/{id}")]
    public ActionResult<VoucherView> Find(int id)
    {
        var voucher = voucherRepository
            .FindById(id);

        if (voucher == null)
        {
            return NotFound("Element not found");
        }

        return _toMapperVoucherView.Mapper(voucher);
    }

    /// <summary>
    /// Delete a voucher by its ID
    /// </summary>
    /// <param name="id">Voucher ID</param>
    /// <returns>The voucher deleted</returns>
    /// <response code="200">Returns the voucher successfully deleted</response>
    /// <response code="404">If the voucher is not found</response>
    /// <remarks>
    /// This endpoint is restricted to users with the "Administrator" role.
    /// </remarks>
    [HttpDelete]
    [Authorize(Roles = "Administrator")] // Restrict access to administrators
    [Route("/voucher/delete/{id}")]
    public ActionResult<VoucherView> Delete(int id)
    {
        var voucherDeleted = voucherRepository.Delete(id);
        if (voucherDeleted == null)
        {
            return NotFound("Element not found");
        }

        return Ok(
            _toMapperVoucherView.Mapper(voucherDeleted)
        );
    }
}
