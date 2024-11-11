using eShop.Application.Utilities;
using eShop.Domain.DTOs;
using eShop.FilesStorageApi.Commands;
using eShop.FilesStorageApi.Queries;
using eShop.FilesStorageApi.Services;

namespace eShop.FilesStorageApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
internal sealed class FilesController(IStoreService storeService,
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
    
    [HttpGet("get-user-avatar/{userId:guid}")]
    public async ValueTask<ActionResult> GetUserAvatarAsync(Guid userId)
    {
        var response = await sender.Send(new GetUserAvatarQuery(userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpPost("upload-product-images")]
    public async ValueTask<ActionResult<ResponseDto>> UploadProductImagesAsync([FromForm] IFormFileCollection files, [FromForm] Guid productId)
    {
        var response = await sender.Send(new UploadProductImagesCommand(files, productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpPost("upload-user-avatar")]
    public async ValueTask<ActionResult<ResponseDto>> UploadUserAvatarAsync([FromForm] IFormFile file, [FromForm] Guid userId)
    {
        var response = await sender.Send(new UploadUserAvatarCommand(file, userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpDelete("delete-product-images/{productId:guid}")]
    public async ValueTask<ActionResult<ResponseDto>> DeleteProductImagesAsync(Guid productId)
    {
        var response = await sender.Send(new DeleteProductImagesCommand(productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpDelete("delete-user-avatar/{userId:guid}")]
    public async ValueTask<ActionResult<ResponseDto>> DeleteUserAvatarAsync(Guid userId)
    {
        var response = await sender.Send(new DeleteUserAvatarCommand(userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}