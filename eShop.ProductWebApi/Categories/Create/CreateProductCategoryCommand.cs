using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Data;
using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace eShop.ProductWebApi.Categories.Create
{
    public record CreateProductCategoryCommand(ProductCategoryDto CategoryDto) : IRequest<Result<ProductCategoryEntity>>;

    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Result<ProductCategoryEntity>>
    {
        private readonly IValidator<ProductCategoryDto> validator;
        private readonly IMapper mapper;
        private readonly ProductDbContext dbContext;

        public CreateProductCategoryCommandHandler(
            IValidator<ProductCategoryDto> validator,
            IMapper mapper,
            ProductDbContext dbContext)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<Result<ProductCategoryEntity>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CategoryDto);

            if (!validationResult.IsValid)
            {
                return new Result<ProductCategoryEntity>(
                    new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var category = mapper.Map<ProductCategoryEntity>(request.CategoryDto);

            try
            {
                var entity = await dbContext.Categories.AddAsync(category);
                var creationResult = await dbContext.SaveChangesAsync();

                if (creationResult > 0)
                    return new Result<ProductCategoryEntity>(entity.Entity);

                return new Result<ProductCategoryEntity>(new NotCreatedProductCategoryException());
            }
            catch (Exception ex)
            {
                return new Result<ProductCategoryEntity>(ex);
            }

        }
    }
}
