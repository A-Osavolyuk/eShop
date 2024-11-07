namespace eShop.Domain.Responses.Files;

public class UploadProductImagesResponse : ResponseBase
{
    public List<string> Images { get; set; }
}