namespace eShop.Domain.Responses.FilesApi.Files;

public class UploadProductImagesResponse : ResponseBase
{
    public List<string> Images { get; set; }
}