using CloudinaryDotNet.Actions;

namespace Taller1.Service;

public interface IImageService
{

    void Connect();

    Task<ImageUploadResult> Upload(IFormFile formFile);

}