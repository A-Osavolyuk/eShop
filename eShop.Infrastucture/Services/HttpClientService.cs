using HttpMethods = eShop.Domain.Enums.HttpMethods;

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

        public async ValueTask<ResponseDto> SendAsync(RequestDto request, bool withBearer = true)
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
                    .WithErrorMessage(ex.Message)
                    .Build();
            }
        }

        public async ValueTask<ResponseDto> SendFilesAsync(FileRequestDto request, bool withBearer = true)
        {
            try
            {
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "multipart/form-data");
                message.RequestUri = new Uri(request.Url);
                message.Method = request.Method switch
                {
                    HttpMethods.POST => HttpMethod.Post,
                    HttpMethods.PUT => HttpMethod.Put,
                    HttpMethods.DELETE => HttpMethod.Delete,
                    HttpMethods.GET => HttpMethod.Get,
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
                    .WithErrorMessage(ex.Message)
                    .Build();
            }
        }

        private async ValueTask<ResponseDto> HandleStatusCode(HttpResponseMessage httpResponse)
        {
            var json = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDto>(json);

            return httpResponse.StatusCode switch
            {
                HttpStatusCode.InternalServerError => new ResponseBuilder().Failed()
                    .WithErrorMessage(response!.ErrorMessage).Build(),
                HttpStatusCode.NotFound => new ResponseBuilder().Failed().WithErrorMessage(response!.ErrorMessage)
                    .Build(),
                HttpStatusCode.Forbidden => new ResponseBuilder().Failed().WithErrorMessage("Forbidden").Build(),
                HttpStatusCode.Unauthorized => new ResponseBuilder().Failed().WithErrorMessage($"Unauthorized").Build(),
                HttpStatusCode.BadRequest => response.Errors.Any()
                    ? new ResponseBuilder().Failed().WithErrorMessage(response!.ErrorMessage + " " + string.Join(';', response.Errors))
                        .WithErrors(response.Errors).Build()
                    : new ResponseBuilder().Failed().WithErrorMessage(response!.ErrorMessage).Build(),
                _ => response
            };
        }
    }
}