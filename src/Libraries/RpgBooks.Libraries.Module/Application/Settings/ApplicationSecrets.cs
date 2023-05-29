namespace RpgBooks.Libraries.Module.Application.Settings;

/// <summary>
/// Contains the required secrets data for the application's current environment configuration.
/// </summary>
public sealed class ApplicationSecrets
{
    /// <summary>
    /// Gets authentication secret key.
    /// </summary>
    public string AuthenticationSecret { get; init; } = default!;

    /// <summary>
    /// Gets tokens protection secret.
    /// </summary>
    public string TokenProtectionSecret { get; init; } = default!;

    /// <summary>
    /// Gets the password protection secret.
    /// </summary>
    public string PasswordProtectionSecret { get; init; } = default!;

    /// <summary>
    /// Gets MSSQL default connection string.
    /// </summary>
    public string DefaultConnectionString { get; init; } = default!;

    /// <summary>
    /// Gets the publishable Stripe API key.
    /// </summary>
    public string StripePublishableKey { get; init; } = default!;

    /// <summary>
    /// Gets the secret Stripe API key.
    /// </summary>
    public string StripeSecretKey { get; init; } = default!;

    /// <summary>
    /// Gets cloud file storage access secret key. Used for encrypting the file download access string.
    /// </summary>
    public string CloudFileStorageSecret { get; init; } = default!;

    /// <summary>
    /// Gets connection string for the Azure storage account. Used for blob storage.
    /// </summary>
    public string CloudFileStorageConnectionString { get; init; } = default!;

    /// <summary>
    /// Gets the secret SendGrid API key.
    /// </summary>
    public string SendGridSecretKey { get; init; } = default!;
}
