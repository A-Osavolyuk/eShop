using Unit = LanguageExt.Unit;
using LanguageExt.Common;
using MediatR;
using eShop.CartWebApi.Repositories;
using eShop.Domain.DTOs;

namespace eShop.CartWebApi.Queries.Cart
{
    public record GetCartByUserIdQuery(Guid UserId) : IRequest<Result<CartDTO>>;

    public class GetCartByUserIdQueryHandler(ICartRepository repository) : IRequestHandler<GetCartByUserIdQuery, Result<CartDTO>>
    {
        private readonly ICartRepository repository = repository;

        public async Task<Result<CartDTO>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetCartByUserIdAsync(request.UserId);
            return result;
        }
    }
}
