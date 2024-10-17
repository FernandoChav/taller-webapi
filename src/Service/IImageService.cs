using CloudinaryDotNet.Actions;

namespace Taller1.Service;

public interface IImageService
{

    Task<ImageUploadResult> Upload(IFormFile formFile);

}