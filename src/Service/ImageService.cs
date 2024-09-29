using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Taller1.Model;

namespace Taller1.Service
{
    
    public class ImageService
    {
        private const int MaxSize = 10 * 1024 * 1024;
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

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
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
        }
        
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