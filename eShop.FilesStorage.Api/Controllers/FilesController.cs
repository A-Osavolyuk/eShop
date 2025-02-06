using eShop.Application.Utilities;
using eShop.Domain.Common.Api;
using eShop.Domain.DTOs;
using eShop.FilesStorage.Api.Commands;
using eShop.FilesStorage.Api.Interfaces;
using eShop.FilesStorage.Api.Queries;
using eShop.FilesStorage.Api.Services;

namespace eShop.FilesStorage.Api.Controllers;

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
    
    [HttpGet("get-user-avatar/{userId:guid}")]
    public async ValueTask<ActionResult> GetUserAvatarAsync(Guid userId)
    {
        var response = await sender.Send(new GetUserAvatarQuery(userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpPost("upload-product-images/{productId:guid}")]
    public async ValueTask<ActionResult<Response>> UploadProductImagesAsync(IFormFileCollection files, Guid productId)
    {
        var response = await sender.Send(new UploadProductImagesCommand(files, productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpPost("upload-user-avatar/{userId:guid}")]
    public async ValueTask<ActionResult<Response>> UploadUserAvatarAsync(IFormFile file, Guid userId)
    {
        var response = await sender.Send(new UploadUserAvatarCommand(file, userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpDelete("delete-product-images/{productId:guid}")]
    public async ValueTask<ActionResult<Response>> DeleteProductImagesAsync(Guid productId)
    {
        var response = await sender.Send(new DeleteProductImagesCommand(productId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpDelete("delete-user-avatar/{userId:guid}")]
    public async ValueTask<ActionResult<Response>> DeleteUserAvatarAsync(Guid userId)
    {
        var response = await sender.Send(new DeleteUserAvatarCommand(userId));
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}