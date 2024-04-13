using eShop.ProductWebApi.Repositories;
using Unit = LanguageExt.Unit;
using MediatR;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record DeleteBrandCommand(Guid Id) : IRequest<Result<Unit>>;

    public class DeleteBrandCommandHandler(IBrandsRepository repository) : IRequestHandler<DeleteBrandCommand, Result<Unit>>
    {
        private readonly IBrandsRepository repository = repository;

        public async Task<Result<Unit>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBrandAsync(request.Id);
            return result;
        }
    }
}
