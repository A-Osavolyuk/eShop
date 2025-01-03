using eShop.Domain.Common.Api;
using HttpMethods = eShop.Domain.Enums.HttpMethods;

namespace eShop.Infrastructure.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient httpClient;
    private readonly ITokenProvider tokenProvider;

    public HttpClientService(IHttpClientFactory clientFactory, ITokenProvider tokenProvider)
    {
        httpClient = clientFactory.CreateClient("eShop.Client");
        this.tokenProvider = tokenProvider;
    }

    public async ValueTask<Response> SendAsync(Request request, bool withBearer = true)
    {
        try
        {
            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            if (withBearer)
            {
                var token = AuthenticationHandler.Token;
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            message.RequestUri = new Uri(request.Url);

            if (request.Data is not null)
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                    Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = default!;

            message.Method = request.Method switch
            {
                HttpMethods.Post => HttpMethod.Post,
                HttpMethods.Delete => HttpMethod.Delete,
                HttpMethods.Put => HttpMethod.Put,
                _ => HttpMethod.Get,
            };

            httpResponse = await httpClient.SendAsync(message);

            return await HandleStatusCode(httpResponse);
        }
        catch (Exception ex)
        {
            return new ResponseBuilder()
                .Failed()
                .WithMessage(ex.Message)
                .Build();
        }
    }

    public async ValueTask<Response> SendFilesAsync(FileRequest request, bool withBearer = true)
    {
        try
        {
            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "multipart/form-data");
            message.RequestUri = new Uri(request.Url);
            message.Method = request.Method switch
            {
                HttpMethods.Post => HttpMethod.Post,
                HttpMethods.Put => HttpMethod.Put,
                HttpMethods.Delete => HttpMethod.Delete,
                HttpMethods.Get => HttpMethod.Get,
                _ => throw new Exception("Invalid HTTP method"),
            };

            if (withBearer)
            {
                var token = AuthenticationHandler.Token;
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            var content = new MultipartFormDataContent();

            if (request.Data.Files.Any())
            {
                foreach (var file in request.Data.Files)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, "files", file.Name);
                }
            }
            else
            {
                var fileContent = new StreamContent(request.Data.File.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(request.Data.File.ContentType);
                content.Add(fileContent, "file", request.Data.File.Name);
            }

            message.Content = content;

            var httpResponse = await httpClient.SendAsync(message);

            return await HandleStatusCode(httpResponse);
        }
        catch(Exception ex)
        {
            return new ResponseBuilder()
                .Failed()
                .WithMessage(ex.Message)
                .Build();
        }
    }

    private async ValueTask<Response> HandleStatusCode(HttpResponseMessage httpResponse)
    {
        var json = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(json);

        return httpResponse.StatusCode switch
        {
            HttpStatusCode.InternalServerError => new ResponseBuilder().Failed()
                .WithMessage(response!.Message).Build(),
            HttpStatusCode.NotFound => new ResponseBuilder().Failed().WithMessage(response!.Message).Build(),
            HttpStatusCode.Forbidden => new ResponseBuilder().Failed().WithMessage("Forbidden").Build(),
            HttpStatusCode.Unauthorized => new ResponseBuilder().Failed().WithMessage("Unauthorized").Build(),
            HttpStatusCode.BadRequest => new ResponseBuilder().Failed().WithMessage("Bad Request").Build(),
            _ => response!
        };
    }
}