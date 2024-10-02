using AutoMapper;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Brand;
using FluentValidation;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record CreateBrandCommand(CreateBrandRequest Request) : IRequest<Result<Unit>>;

    public class CreateBrandCommandHandler(
        IValidator<CreateBrandRequest> validator,
        IMapper mapper,
        ProductDbContext context,
        ILogger<CreateBrandCommandHandler> logger) : IRequestHandler<CreateBrandCommand, Result<Unit>>
    {
        private readonly IValidator<CreateBrandRequest> validator = validator;
        private readonly IMapper mapper = mapper;
        private readonly ProductDbContext context = context;
        private readonly ILogger<CreateBrandCommandHandler> logger = logger;

        public async Task<Result<Unit>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("create new brand");
            try
            {
                logger.LogInformation("Attempting to create new brand. Request ID {requestId}", request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<Unit>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var brand = mapper.Map<Brand>(request.Request);
                await context.Brands.AddAsync(brand);
                await context.SaveChangesAsync();

                logger.LogInformation("Brand was successfully created. Request ID {requestId}", request.Request.RequestId);
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
