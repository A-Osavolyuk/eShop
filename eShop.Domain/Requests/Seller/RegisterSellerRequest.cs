﻿namespace eShop.Domain.Requests.Seller;

public class RegisterSellerRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Article { get; set; } = string.Empty;
}