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

    [HttpPost]
    [Route("/voucher/create")]
    public VoucherCreation Create(VoucherCreation creationVoucher)
    {
        var voucher = _toMapperVoucher.Mapper(creationVoucher);
        voucherRepository.Push(voucher);

        return creationVoucher;
    }

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

    [HttpDelete]
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