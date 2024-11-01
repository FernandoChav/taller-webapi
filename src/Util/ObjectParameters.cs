namespace Taller1.Util;

/// <summary>
/// This class contains a set parameters 
/// </summary>
/// <param name="objects">A set parameters in format key-value, where key is the name parameter </param>
public class ObjectParameters(IDictionary<string, object> objects)
{
    /// <summary>
    /// Get a value from parameter in string type
    /// </summary>
    /// <param name="key">The parameter name</param>
    /// <returns>A value associated at this name parameter</returns>
    public string GetString(string key)
    {
        return (string)objects[key];
    }

    /// <summary>
    /// Get a value from parameter in int type
    /// </summary>
    /// <param name="key">The parameter name</param>
    /// <returns>A value associated at this name parameter</returns>
    public int GetInt(string key)
    {
        return (int)objects[key];
    }

    /// <summary>
    /// Get a value from parameter in object type
    /// </summary>
    /// <param name="key">The parameter name</param>
    /// <returns>A value associated at this name parameter</returns>
    public object Get(string key)
    {
        return objects[key];
    }

    /// <summary>
    /// Check if a parameter exists
    /// </summary>
    /// <param name="key">The name parameter</param>
    /// <returns>A boolean that is true if exists</returns>
    
    public bool Exists(string key)
    {
        return objects.ContainsKey(key);
    }

    /// <summary>
    /// Add a new parameter for collection parameters
    /// </summary>
    /// <param name="key">The name parameter</param>
    /// <param name="obj">The value parameter</param>
    /// <returns></returns>
    
    public ObjectParameters AddParameter(string key, object obj)
    {
        objects[key] = obj;
        return this;
    }

    /// <summary>
    /// Execute action if this name parameter exists
    /// </summary>
    /// <param name="key">A name parameter</param>
    /// <param name="action">Action in format callback</param>
    
    public void ExecuteIfExists(string key,
        Action<object> action)
    {
        if (objects.TryGetValue(key, out object? obj))
        {
            action.Invoke(obj);
        }
    }

    /// <summary>
    /// Create a new instance from ObjectParameters
    /// </summary>
    /// <returns></returns>
    
    public static ObjectParameters Create()
    {
        return new ObjectParameters(new Dictionary<string, object>());
    }
    
}