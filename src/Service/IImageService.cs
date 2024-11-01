using CloudinaryDotNet.Actions;

namespace Taller1.Service;

/// <summary>
/// This service provide a way for manage image to extern repository
/// </summary>

public interface IImageService
{

    /// <summary>
    /// Connect to extern service
    /// </summary>
    
    void Connect();
    
    /// <summary>
    /// Upload a image 
    /// </summary>
    /// <param name="formFile">Image as file</param>
    /// <returns>A result image</returns>

    Task<ImageUploadResult> Upload(IFormFile formFile);

    /// <summary>
    /// Destroy image
    /// </summary>
    /// <param name="id">Image id for deleted</param>
    /// <returns>A collection of data about image deleted</returns>
    
    Task<DeletionResult> Destroy(string id);

}