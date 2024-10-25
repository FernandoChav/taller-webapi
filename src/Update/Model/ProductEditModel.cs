using Taller1.Service;
using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Update.Model;

public class ProductEditModel : IUpdateModel<Product>
{
    private readonly IImageService _imageService;

    public ProductEditModel(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async void Edit(ObjectParameters parameters
        , Product modelObject)
    {
        parameters.ExecuteIfExists("Name", obj => { modelObject.Name = (string)obj; });
        parameters.ExecuteIfExists("Stock", obj => { modelObject.Stock = (int)obj; });
        parameters.ExecuteIfExists("Price", obj => { modelObject.Price = (int)obj; });
        
        parameters.ExecuteIfExists("ProductType", obj =>
        {
            var str = obj as string;

            if (Enum.TryParse(str, out ProductType productType))
            {
                modelObject.ProductType = productType;
            }
        });

        if (parameters.Exists("Image"))
        {
            var image = (IFormFile)parameters.Get("Image");
            var result = await _imageService.Upload(image);
            
            var publicId = result.PublicId;
            var absoluteUri = result.SecureUrl.AbsoluteUri;

            modelObject.IdImage = publicId;
            modelObject.AbsoluteUri = absoluteUri;
        }
        
   
    }
}