
namespace Taller1.Model
{

    /// <summary>
    /// This class represent a set settings for connect cloudinary settings
    /// </summary>
    
    public class CloudinarySettings
    {

        /// <value>Attribute <c>CloudName</c> represent host name</value>
        public string CloudName { get; set; }
        /// <value>Attribute <c>ApiKey</c> represent the key for access service cloudinary</value>
        public string ApiKey { get; set; }
        /// <value>Attribute <c>ApiSecret</c> represent the api secret for access service cloudinary</value>
        public string ApiSecret { get; set; }

    }
}