﻿using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Comments
{
    public record class DeleteCommentsRequest : RequestBase
    {
        public Guid ProductId { get; set; }
    }
}