using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Data;
using eShop.ProductWebApi.Repositories.Interfaces;
using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace eShop.ProductWebApi.Categories.Create
{
    public record CreateProductCategoryCommand(ProductCategoryDto CategoryDto) : IRequest<Result<CategoryEntity>>;

    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Result<CategoryEntity>>
    {
        private readonly IValidator<ProductCategoryDto> validator;
        private readonly IMapper mapper;
        private readonly ICategoriesRepository repository;

        public CreateProductCategoryCommandHandler(
            IValidator<ProductCategoryDto> validator,
            IMapper mapper,
            ICategoriesRepository repository)
        {
            this.validator = validator;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<CategoryEntity>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CategoryDto);

            if (!validationResult.IsValid)
            {
                return new Result<CategoryEntity>(
                    new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var category = mapper.Map<CategoryEntity>(request.CategoryDto);

            var result = await repository.CreateCategoryAsync(category);

            return result.Match<Result<CategoryEntity>>(s => new (s), f => new (f));

        }
    }
}
