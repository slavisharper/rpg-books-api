namespace RpgBooks.Modules.Identity.Domain.Services;

using System;

/// <summary>
/// Token data model.
/// </summary>
/// <param name="Value">Token value.</param>
/// <param name="ExpirationTime">
/// Token expiration date time in UTC
/// <para>Expiration time is null if the token does not expire.</para>
/// </param>
public sealed record TokenModel(string Value, DateTimeOffset? ExpirationTime);