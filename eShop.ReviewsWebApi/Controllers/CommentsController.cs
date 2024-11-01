using eShop.Domain.Requests.Comments;
using eShop.ReviewsWebApi.Commands.Comments;

namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommentsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;


        [HttpPost("create-comment")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateCommentAsync(CreateCommentRequest request)
        {
            var result = await sender.Send(new CreateCommentCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}
