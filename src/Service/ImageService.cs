using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Taller1.Model;

namespace Taller1.Service
{
    
    public class ImageService : IImageService 
    {
        private const int MaxSize = 10 * 1024 * 1024;
        private const int MinSize = 0;
        
        private const int Height = 500;
        private const int Width = 500;
        private const string Crop = "fill";
        private const string Gravity = "face";
        private const string Folder = "ucn-store";
        
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> Upload(IFormFile formFile)
        {

            var result = new ImageUploadResult();
            var length = formFile.Length;
            
            if (!(MinSize <= length &&  length <= MaxSize))
            {
                return result;
            }

            await using var stream = formFile.OpenReadStream();
            var parameters = new ImageUploadParams
            {
                File = new FileDescription(formFile.FileName, stream),
                Transformation = new Transformation()
                    .Width(500)
                    .Height(500)
                    .Crop(Crop)
                    .Gravity(Gravity),
                Folder = Folder
            };
            
            return await _cloudinary.UploadAsync(parameters);
        }

        /*public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            if (extension != ".jpg" && extension != ".png")
            {
                throw new Exception("Invalid file format. Only .jpg and .png are allowed.");
            }

            if (fileStream.Length > MaxSize)
            {
                throw new Exception("File size exceeds the 10 MB limit.");
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                Transformation = new Transformation().Quality(80)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            throw new Exception("Error uploading image to Cloudinary.");
        }*/
        
       /* [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var imageUrl = await _cloudinary.UploadImageAsync(stream, file.FileName);
                    return Ok(new { Url = imageUrl });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }*/
       
    }
}