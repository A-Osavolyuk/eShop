using AutoMapper;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Brand;
using eShop.ProductWebApi.Exceptions;
using FluentValidation;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record UpdateBrandCommand(UpdateBrandRequest Request) : IRequest<Result<Unit>>;

    public class UpdateBrandCommandHandler(
        IValidator<UpdateBrandRequest> validator,
        IMapper mapper,
        ILogger<UpdateBrandCommandHandler> logger,
        ProductDbContext context) : IRequestHandler<UpdateBrandCommand, Result<Unit>>
    {
        private readonly IValidator<UpdateBrandRequest> validator = validator;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<UpdateBrandCommandHandler> logger = logger;
        private readonly ProductDbContext context = context;

        public async Task<Result<Unit>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("update brand with ID {0}", request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to update brand with ID {id}. Request ID {requestID}.", request.Request.Id, request.Request.RequestId);
                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<Unit>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var brandExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == request.Request.Id);

                if (!brandExists)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundBrandException(request.Request.Id), actionMessage, request.Request.RequestId);
                }

                var brand = mapper.Map<Brand>(request.Request);
                context.Brands.Update(brand);
                await context.SaveChangesAsync();

                logger.LogInformation("Brand with ID {id} was successfully updated. Request ID {requestID}.", request.Request.Id, request.Request.RequestId);
                return new(new Unit());
            }
            catch (DbUpdateException dbUpdateException)
            {
                return logger.LogErrorWithException<Unit>(dbUpdateException, actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<Unit>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
