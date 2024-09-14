using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using Taller1.src.Model;
namespace Taller1.src.Service
{
    public class ImageService
    {
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

            if (fileStream.Length > 10 * 1024 * 1024)
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
    }
}