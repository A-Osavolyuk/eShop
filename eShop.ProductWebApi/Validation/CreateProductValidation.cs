using eShop.Application.Validation.Products;
using eShop.ProductWebApi.Commands;
using FluentValidation;

namespace eShop.ProductWebApi.Validation;

public class CreateProductValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Request).SetValidator(new CreateProductValidator());
    }
}