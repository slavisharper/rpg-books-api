namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Login record entity.
/// <para>Holds information about user login.</para>
/// </summary>
internal sealed class LoginRecord : BaseEntity<int>
{
    internal LoginRecord(User user, string sessionId, string? IpAddress, string? userAgent)
    {
        this.LoginTime = DateTimeOffset.UtcNow;
        this.IpAddress = IpAddress;
        this.UserAgent = userAgent;
        this.User = user;
        this.SessionId = sessionId;

        this.Validate();
    }

    private LoginRecord(DateTimeOffset loginTime, string sessionId, string? ipAddress, string? userAgent)
    {
        this.LoginTime = loginTime;
        this.IpAddress = ipAddress;
        this.UserAgent = userAgent;
        this.SessionId = sessionId;

        this.User = default!;
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

    /// <summary>
    /// Gets the user that has logged in.
    /// </summary>
    public User User { get; private set; }

    private void Validate()
    {
        LoginRecordValidation.EnsureThat.HasValidIpAddress(this.IpAddress);
        LoginRecordValidation.EnsureThat.HasValidUserAgent(this.UserAgent);
    }   
}
