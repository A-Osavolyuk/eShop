using eShop.Application.Utilities;
using eShop.Domain.DTOs;
using eShop.FilesStorageWebApi.Commands;
using eShop.FilesStorageWebApi.Queries;

namespace eShop.FilesStorageWebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class FilesController(IStoreService storeService,
    ISender sender) : ControllerBase
{
    private readonly IStoreService storeService = storeService;
    private readonly ISender sender = sender;

    [HttpGet("get-product-images/{productId:guid}")]
    public async ValueTask<ActionResult> GetProductImagesAsync(Guid productId)
    {
        var response = await sender.Send(new GetProductImagesQuery(productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpPost("upload-product-images")]
    public async ValueTask<ActionResult<ResponseDTO>> UploadProductImagesAsync([FromForm] IFormFileCollection files, [FromForm] Guid productId)
    {
        var response = await sender.Send(new UploadProductImagesCommand(files, productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpDelete("delete-product-images/{productId:guid}")]
    public async ValueTask<ActionResult<ResponseDTO>> DeleteProductImagesAsync(Guid productId)
    {
        var response = await sender.Send(new DeleteProductImagesCommand(productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}