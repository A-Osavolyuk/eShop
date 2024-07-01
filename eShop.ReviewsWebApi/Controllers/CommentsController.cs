using eShop.ReviewsWebApi.Queries.Comments;

namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class CommentsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-comments-by-review-id/{Id:guid}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetCommentsWithReviewId([FromRoute] Guid Id)
        {
            var result = await sender.Send(new GetCommentListWirthReviewIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
