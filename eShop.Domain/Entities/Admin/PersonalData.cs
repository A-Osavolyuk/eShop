﻿using System.Text.Json.Serialization;
using eShop.Domain.Entities.Auth;

namespace eShop.Domain.Entities.Admin
{
    public record class PersonalData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);

        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
        [JsonIgnore]
        public AppUser? User { get; set; }
    }
}
