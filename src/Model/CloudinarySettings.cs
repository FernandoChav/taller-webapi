
namespace Taller1.Model
{

    /// <summary>
    /// This class represent a set settings for connect cloudinary settings
    /// </summary>
    
    public class CloudinarySettings
    {

        /// <summary>
        /// This is a URL used for connecting to API Clodinary
        /// This URL follow the next format:
        ///     cloudinary://my_key:my_secret@my_cloud_name
        /// </summary>
        
        public required string Url { get; set; }

    }
    
}