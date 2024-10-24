namespace Taller1.Util;

public class ObjectParameters
{

    private IDictionary<string, object>
        _objects;
    
    public ObjectParameters(IDictionary<string, object> objects)
    {
        _objects = objects;
    }

    public ObjectParameters()
    {
        _objects = new Dictionary<string, object>();
    }

    public string GetString(string key)
    {
        return (string) _objects[key];
    }

    public int GetInt(string key)
    {
        return (int)_objects[key];
    }

    public ObjectParameters AddParameter(string key, object obj)
    {
        _objects[key] = obj;
        return this;
    }

    public static ObjectParameters Create()
    {
        return new ObjectParameters();
    }
    
}