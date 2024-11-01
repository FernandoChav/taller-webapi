using System.Collections.Immutable;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Taller1.Model;

namespace Taller1.Service
{
    public class CloudinaryImageService : IImageService
    {
        private const int MaxSize = 10 * 1024 * 1024;
        private const int MinSize = 0;

        private static readonly
            ImmutableArray<string> ExtensionAble = [".jpg", ".png"];

        private const int Height = 500;
        private const int Width = 500;
        private const string Crop = "fill";
        private const string Gravity = "face";
        private const string Folder = "ucn-store";

        private readonly IOptions<CloudinarySettings> _config;
        private Cloudinary _cloudinary;

        public CloudinaryImageService(IOptions<CloudinarySettings> config)
        {
            _config = config;
            Connect();
        }

        public void Connect()
        {
            _cloudinary = new Cloudinary(_config.Value.Url);
        }

        public async Task<ImageUploadResult> Upload(IFormFile formFile)
        {
            var result = new ImageUploadResult();
            var length = formFile.Length;
            var extension = Path.GetExtension(formFile.FileName);

            if (!(MinSize <= length && length <= MaxSize) ||
                !(ExtensionAble.Contains(extension)))
            {
                Console.WriteLine("ERROR");
                return result;
            }

            await using var stream = formFile.OpenReadStream();
            var parameters = new ImageUploadParams
            {
                File = new FileDescription(formFile.FileName, stream),
                Transformation = new Transformation()
                    .Width(Width)
                    .Height(Height)
                    .Crop(Crop)
                    .Gravity(Gravity),
                Folder = Folder
            };

            return await _cloudinary.UploadAsync(parameters);
        }

        public async Task<DeletionResult> Destroy(string id)
        {
            var parameters = new DeletionParams(id);
            return await _cloudinary.DestroyAsync(parameters);
        }
    }
}