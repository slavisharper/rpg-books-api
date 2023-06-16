namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Login record entity.
/// <para>Holds information about user login.</para>
/// </summary>
public sealed class LoginRecord : BaseEntity<int>
{
    internal LoginRecord(string sessionId, string? IpAddress, string? userAgent)
    {
        this.LoginTime = DateTimeOffset.UtcNow;
        this.IpAddress = IpAddress;
        this.UserAgent = userAgent;
        this.SessionId = sessionId;

        this.Validate();
    }

    private LoginRecord(DateTimeOffset loginTime, string sessionId, string? ipAddress, string? userAgent)
    {
        this.LoginTime = loginTime;
        this.IpAddress = ipAddress;
        this.UserAgent = userAgent;
        this.SessionId = sessionId;
    }

    /// <summary>
    /// Gets the login time in UTC.
    /// </summary>
    public DateTimeOffset LoginTime { get; private set; }

    /// <summary>
    /// Gets the IP address of the user.
    /// </summary>
    public string? IpAddress { get; private set; }

    /// <summary>
    /// Gets the user agent of the user.
    /// </summary>
    public string? UserAgent { get; private set; }

    /// <summary>
    /// Gets the id of the new user login session.
    /// <para>This Id is persisted throw the refresh token process for the given session.</para>
    /// </summary>
    public string SessionId { get; private set; }

    private void Validate()
    {
        LoginRecordValidation.EnsureThat.HasValidIpAddress(this.IpAddress);
        LoginRecordValidation.EnsureThat.HasValidUserAgent(this.UserAgent);
    }   
}
