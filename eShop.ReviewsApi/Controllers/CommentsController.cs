﻿using eShop.Domain.Requests.Comments;
using eShop.ReviewsApi.Commands.Comments;
using eShop.ReviewsApi.Queries.Comments;

namespace eShop.ReviewsApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-comments/{productId:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetCommentAsync(Guid productId)
        {
            var result = await sender.Send(new GetCommentsQuery(productId));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPost("create-comment")]
        public async ValueTask<ActionResult<ResponseDto>> CreateCommentAsync([FromBody] CreateCommentRequest request)
        {
            var result = await sender.Send(new CreateCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPut("update-comment")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            var result = await sender.Send(new UpdateCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpDelete("delete-comment")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteCommentAsync([FromBody] DeleteCommentRequest request)
        {
            var result = await sender.Send(new DeleteCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}