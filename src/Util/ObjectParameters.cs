namespace Taller1.Util;

public class ObjectParameters(IDictionary<string, object> objects)
{
    public string GetString(string key)
    {
        return (string)objects[key];
    }

    public int GetInt(string key)
    {
        return (int)objects[key];
    }

    public object Get(string key)
    {
        return objects[key];
    }

    public bool Exists(string key)
    {
        return objects.ContainsKey(key);
    }

    public ObjectParameters AddParameter(string key, object obj)
    {
        objects[key] = obj;
        return this;
    }

    public void ExecuteIfExists(string key,
        Action<object> action)
    {
        if (objects.TryGetValue(key, out object? obj))
        {
            action.Invoke(obj);
        }
    }

    public static ObjectParameters Create()
    {
        return new ObjectParameters(new Dictionary<string, object>());
    }
}