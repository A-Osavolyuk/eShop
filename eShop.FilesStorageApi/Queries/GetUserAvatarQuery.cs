﻿using eShop.Domain.Exceptions;

namespace eShop.FilesStorageApi.Queries;

internal sealed record GetUserAvatarQuery(Guid UserId) : IRequest<Result<string>>;

internal sealed class GetUserAvatarQueryHandler(IStoreService service) : IRequestHandler<GetUserAvatarQuery, Result<string>>
{
    private readonly IStoreService service = service;

    public async Task<Result<string>> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await service.GetUserAvatarAsync(request.UserId);

            if (string.IsNullOrWhiteSpace(response))
            {
                return new(new FailedOperationException($"Cannot get avatar of user with ID {request.UserId}"));
            }
            
            return new(response);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}