namespace RpgBooks.Modules.Identity.Domain.Services;

using System;

public sealed record TokenModel(string Value, DateTimeOffset? ExpirationTime);