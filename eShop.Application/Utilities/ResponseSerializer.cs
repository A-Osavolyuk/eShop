using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace eShop.Application.Utilities;

public static class ResponseConverter
{
    public static T Deserialize<T> ([NotNull] object obj)
    {
        return JsonConvert.DeserializeObject<T>(Convert.ToString(obj)!)!;
    } 
}