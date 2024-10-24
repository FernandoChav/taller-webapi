using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Taller1.Model;
using Taller1.Service;

namespace Taller1.Controller;

[ApiController]
[Route("api/[controller]")]
public class VoucherController(
         IObjectRepository<Voucher, VoucherEdit> voucherRepository
    ) : ControllerBase
{
    
    [HttpPost]
    [Route("/voucher/create")]
    public CreationVoucher Create(CreationVoucher creationVoucher)
    {
        var products = new List<VoucherProduct>();
        foreach (var productCreation in creationVoucher.Products)
        {
            var voucherProduct = new VoucherProduct
            {
                Name = productCreation.Name,
                Type = productCreation.Type,
                Price = productCreation.Price,
                Elements = productCreation.Elements,
            };

            products.Add(voucherProduct);
        }

        var voucher = new Voucher
        {
            Date = creationVoucher.Date,
            UserId = creationVoucher.UserId,
            AllProducts = products
        };

        voucherRepository.Push(
            voucher
        );

        return creationVoucher;
    }

    [HttpGet]
    [Route("/voucher/find/{id}")]
    public ActionResult<VoucherResponse> Find(int id)
    {
        var voucher = voucherRepository
            .FindById(id);

        if (voucher == null)
        {
            return NotFound("Element not found");
        }
        
   
        var products = new List<VoucherProductResponse>();
        foreach (var element in voucher.AllProducts)
        {
            products.Add(
                    new VoucherProductResponse
                    {
                           Name = element.Name,
                           Type = element.Type,
                           Elements = element.Elements,
                           Price = element.Price
                    }
                );
        }
        
        var voucherResponse = new VoucherResponse
        {
            CreatedVoucherDate = voucher.Date,
            Products = products
        };

        return voucherResponse;
    }

    [HttpDelete]
    [Route("/voucher/delete/{id}")]
    public ActionResult Delete(int id)
    {
        voucherRepository.Delete(id);
        return Ok();
    }
    
}