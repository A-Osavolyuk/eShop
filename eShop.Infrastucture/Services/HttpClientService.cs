using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace eShop.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenProvider tokenProvider;

        public HttpClientService(IHttpClientFactory clientFactory, ITokenProvider tokenProvider)
        {
            httpClient = clientFactory.CreateClient("eShop.Client");
            this.tokenProvider = tokenProvider;
        }

        public async ValueTask<ResponseDTO?> SendAsync(RequestDto request, bool withBearer = true)
        {
            try
            {
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = await tokenProvider.GetTokenAsync();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(request.Url);

                if (request.Data is not null)
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                        Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = default!;

                message.Method = request.Method switch
                {
                    HttpMethods.POST => HttpMethod.Post,
                    HttpMethods.DELETE => HttpMethod.Delete,
                    HttpMethods.PUT => HttpMethod.Put,
                    _ => HttpMethod.Get,
                };

                httpResponse = await httpClient.SendAsync(message);

                return await HandleStatusCode(httpResponse);
            }
            catch (Exception ex)
            {
                return new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(ex.Message)
                    .Build();
            }
        }

        private async ValueTask<ResponseDTO> HandleStatusCode(HttpResponseMessage httpResponse)
        {
            var response = JsonConvert.DeserializeObject<ResponseDTO>(await httpResponse.Content.ReadAsStringAsync())!;

            return httpResponse.StatusCode switch
            {
                HttpStatusCode.InternalServerError => new ResponseBuilder().Failed().AddErrorMessage($"Internal Server Error. {response!.ErrorMessage}").Build(),
                HttpStatusCode.NotFound => new ResponseBuilder().Failed().AddErrorMessage($"Not Found. {response!.ErrorMessage}").Build(),
                HttpStatusCode.Forbidden => new ResponseBuilder().Failed().AddErrorMessage("Forbidden").Build(),
                HttpStatusCode.Unauthorized => new ResponseBuilder().Failed().AddErrorMessage($"Unauthorized").Build(),
                HttpStatusCode.BadRequest => response.Errors.Any() ? 
                    new ResponseBuilder().Failed().AddErrorMessage($"Bad Request. {response!.ErrorMessage}").AddErrors(response.Errors).Build() :
                    new ResponseBuilder().Failed().AddErrorMessage($"Bad Request. {response!.ErrorMessage}").Build(),
                _ => response
            };
        }
    }
}
