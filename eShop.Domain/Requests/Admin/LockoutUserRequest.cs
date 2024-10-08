﻿using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record LockoutUserRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public bool Permanent { get; set; }
    }
}
