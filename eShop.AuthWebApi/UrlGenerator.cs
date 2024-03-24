using System.Text;

namespace eShop.AuthWebApi.Services.Implementation
{
    public partial class AuthService
    {
        public static class UrlGenerator
        {
            public static string ActionLink(string action, string controller, object values, string scheme, HostString host)
            {

                var queryParams = new StringBuilder("");

                if (values is not null)
                {
                    queryParams.Append("?");

                    var props = values.GetType().GetProperties();

                    for (int i = 0; i < props.Length; i++)
                    {
                        if (i == props.Length - 1)
                            queryParams.Append($"{props[i].Name}={props[i].GetValue(values)}");
                        else
                            queryParams.Append($"{props[i].Name}={props[i].GetValue(values)}&");

                    }
                }

                return $"{scheme}://{host.Host}:{host.Port}/{controller}/{action}{queryParams}";
            }
        }
    }
}
