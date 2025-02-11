using Newtonsoft.Json;

namespace eShop.Application.Utilities;

public static class ResponseConverter
{
    public static T Deserialize<T> (object obj)
    {
        return JsonConvert.DeserializeObject<T>(Convert.ToString(obj)!)!;
    } 
}