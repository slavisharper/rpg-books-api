namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

public sealed class LoginRecord : BaseEntity<int>
{
    internal LoginRecord(User user, string? IpAddress, string? userAgent)
    {
        this.LoginTime = DateTimeOffset.UtcNow;
        this.IpAddress = IpAddress;
        this.UserAgent = userAgent;
        this.User = user;

        this.Validate();
    }

    private LoginRecord(DateTimeOffset loginTime, string? ipAddress, string? userAgent)
    {
        this.LoginTime = loginTime;
        this.IpAddress = ipAddress;
        this.UserAgent = userAgent;

        this.User = default!;
    }

    public DateTimeOffset LoginTime { get; private set; }

    public string? IpAddress { get; private set; }

    public string? UserAgent { get; private set; }

    public User User { get; private set; }

    private void Validate()
    {
        LoginRecordValidation.EnsureThat.HasValidIpAddress(this.IpAddress);
        LoginRecordValidation.EnsureThat.HasValidUserAgent(this.UserAgent);
    }   
}
