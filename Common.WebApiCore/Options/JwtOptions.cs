﻿using System;
using Microsoft.IdentityModel.Tokens;

namespace Common.WebApiCore.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public byte[] AccessSecret { get; set; }
        public byte[] RefreshSecret { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan AccessValidFor { get; set; } = TimeSpan.FromDays(30);
        public TimeSpan RefreshValidFor { get; set; } = TimeSpan.FromDays(90);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime AccessExpiration => IssuedAt.Add(AccessValidFor);
        public DateTime RefreshExpiration => IssuedAt.Add(RefreshValidFor);
        public SigningCredentials AccessSigningCredentials { get; set; }
        public SigningCredentials RefreshSigningCredentials { get; set; }
    }
}