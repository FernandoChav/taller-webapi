using Taller1.Service;
using Taller1.src.Models;

namespace Taller1.Update.Model;

public class ProductEditModel : IUpdateModel<ProductEdit, Product>
{
    private readonly IImageService _imageService;

    public ProductEditModel(IImageService imageService)
    {
        _imageService = imageService;
    }

    public void Edit(ProductEdit editObject, Product modelObject)
    {
        if (editObject.Name != null)
        {
            modelObject.Name = editObject.Name;
        }

        if (editObject.ProductType != null)
        {
            modelObject.ProductType = editObject.ProductType.Value;
        }

        if (editObject.Stock != null)
        {
            modelObject.Stock = editObject.Stock.Value;
        }

        if (editObject.Price != null)
        {
            modelObject.Price = editObject.Price.Value;
        }

        if (editObject.Image != null)
        {
            var task = _imageService.Upload(editObject.Image);
            var result = task.Result;
            
            var publicId = result.PublicId;
            var absoluteUri = result.SecureUrl.AbsoluteUri;

            modelObject.IdImage = publicId;
            modelObject.AbsoluteUri = absoluteUri;
        }
        
    }
}