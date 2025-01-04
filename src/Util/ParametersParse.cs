using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Taller1.Util;

public class ParametersParse
{
    
    private ParametersParse() {}

    public static ObjectParameters Parse(IDictionary<string, object> values)
    {
        var parametersCreated = ObjectParameters.Create();
        foreach (var entry in values)
        {
            var obj = entry.Value;
            if (!(obj is JsonElement))
            {
                continue;
            }
            
            var objJsonElement = (JsonElement)obj;
            var value = objJsonElement.GetString();

            parametersCreated.AddParameter(entry.Key, value);
        }
        
        return parametersCreated;
    }
    
}