using eShop.Domain.Requests.Comments;
using eShop.Domain.Responses.Comments;
using eShop.ReviewsWebApi.Commands.Comments;
using eShop.ReviewsWebApi.Queries.Comments;

namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-comments")]
        public async ValueTask<ActionResult<ResponseDTO>> GetCommentAsync([FromBody] GetCommentsRequest request)
        {
            var result = await sender.Send(new GetCommentsQuery(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPost("create-comment")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateCommentAsync([FromBody] CreateCommentRequest request)
        {
            var result = await sender.Send(new CreateCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPut("update-comment")]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateCommentAsync([FromBody] UpdateCommentRequest request)
        {
            var result = await sender.Send(new UpdateCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpDelete("delete-comment")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteCommentAsync([FromBody] DeleteCommentRequest request)
        {
            var result = await sender.Send(new DeleteCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}
