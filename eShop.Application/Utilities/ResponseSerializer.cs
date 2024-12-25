using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace eShop.Application.Utilities;

public static class ResponseConverter
{
    public static T Deserialize<T> ([NotNull] object obj)
    {
        return JsonConvert.DeserializeObject<T>(Convert.ToString(obj)!)!;
    } 
}