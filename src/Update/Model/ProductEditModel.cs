using Taller1.Service;
using Taller1.src.Models;
using Taller1.Util;

namespace Taller1.Update.Model;
/// <summary>
/// Represents the logic for updating a Product object.
/// </summary>
public class ProductEditModel : IUpdateModel<Product>
{
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductEditModel"/> class.
    /// </summary>
    /// <param name="imageService">The image service used to upload product images.</param>
    public ProductEditModel(IImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Edits the properties of a Product object based on the provided parameters.
    /// </summary>
    /// <param name="parameters">The parameters containing the values to update in the Product object.</param>
    /// <param name="modelObject">The Product object to be updated.</param>
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