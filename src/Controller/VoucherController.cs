using Microsoft.AspNetCore.Mvc;
using Taller1.Model;

namespace Taller1.Controller;

[ApiController]
[Route("api/[controller]")]
public class VoucherController
{

    [HttpPost]
    public Voucher Create(CreationVoucher creationVoucher)
    {
        return null;
    }
    
}