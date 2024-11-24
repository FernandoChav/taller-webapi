using System.Collections.Immutable;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Taller1.Model;

namespace Taller1.Service
{
    /// <summary>
    /// Service for handling image uploads and deletions using Cloudinary.
    /// </summary>
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

        /// <summary>
        /// Initializes the CloudinaryImageService with Cloudinary settings.
        /// </summary>
        /// <param name="config">Cloudinary settings.</param>
        public CloudinaryImageService(IOptions<CloudinarySettings> config)
        {
            _config = config;
            Connect();
        }

        /// <summary>
        /// Connects to Cloudinary using the settings provided in the configuration.
        /// </summary>
        public void Connect()
        {
            _cloudinary = new Cloudinary(_config.Value.Url);
        }

        /// <summary>
        /// Uploads an image to Cloudinary with transformations applied.
        /// Validates file size and extension before uploading.
        /// </summary>
        /// <param name="formFile">The image file to be uploaded.</param>
        /// <returns>ImageUploadResult containing the result of the upload operation.</returns>
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

        /// <summary>
        /// Deletes an image from Cloudinary using the provided image ID.
        /// </summary>
        /// <param name="id">The ID of the image to be deleted.</param>
        /// <returns>DeletionResult containing the result of the delete operation.</returns>
        public async Task<DeletionResult> Destroy(string id)
        {
            var parameters = new DeletionParams(id);
            return await _cloudinary.DestroyAsync(parameters);
        }
    }
}