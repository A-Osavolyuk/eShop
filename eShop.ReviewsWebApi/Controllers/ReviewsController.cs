﻿namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ReviewsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-reviews-by-product-id/{Id:guid}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetReviewListByProductId([FromRoute] Guid Id)
        {
            var result = await sender.Send(new GetReviewListByProductIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-review")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateReview([FromBody] CreateReviewRequest request)
        {
            var result = await sender.Send(new CreateReviewCommand(request));
            return result.Match(
                s => StatusCode(201, new ResponseBuilder().Succeeded().WithResultMessage("Review was successfully created").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("update-review")]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateReview([FromBody] UpdateReviewRequest request)
        {
            var result = await sender.Send(new UpdateReviewCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Review was successfully updated.").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("delete-reviews-with-product-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteReviewsWithProductId([FromRoute] Guid Id)
        {
            var result = await sender.Send(new DeleteReviewsWithProductIdCommand(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage($"Reviews with product id: {Id} were successfully deleted").Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
