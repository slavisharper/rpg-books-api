namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Common.Exceptions;
using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;
using RpgBooks.Libraries.Module.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;
using System;

public class User : BaseEntity<int>, IAggregateRoot, IConcurrentEntity
{
    private readonly ICollection<Claim> claims;
    private readonly ICollection<Role> roles;
    private readonly ICollection<SecurityToken> securityTokens;

    internal User(string email, string passowrdHash)
    {
        this.Email = email;
        this.PasswordHash = passowrdHash;

        this.GenerateSecurityStamp();
        this.ConcurrencyStamp = Array.Empty<byte>();

        this.claims = new HashSet<Claim>();
        this.roles = new HashSet<Role>();
        this.securityTokens = new HashSet<SecurityToken>();
    }

    private User(
        string? honorificTitle,
        string? firstName,
        string? middleName,
        string? lastName,
        bool emailConfirmed,
        string passwordHash,
        int accessFailedCount,
        DateTimeOffset? lastSuccessAccess,
        bool lockoutEnabled,
        DateTimeOffset? lockoutEnd,
        bool phoneNumberConfirmed,
        bool blocked,
        string securityStamp,
        byte[] concurrencyStamp)
    {
        this.HonorificTitle = honorificTitle;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.EmailConfirmed = emailConfirmed;
        this.PasswordHash = passwordHash;
        this.AccessFailedCount = accessFailedCount;
        this.LastSuccessAccess = lastSuccessAccess;
        this.LockoutEnabled = lockoutEnabled;
        this.LockoutEnd = lockoutEnd;
        this.PhoneNumberConfirmed = phoneNumberConfirmed;
        this.Blocked = blocked;
        this.SecurityStamp = securityStamp;
        this.ConcurrencyStamp = concurrencyStamp;

        this.Email = default!;
        this.PhoneNumber = default!;

        this.claims = new HashSet<Claim>();
        this.roles = new HashSet<Role>();
        this.securityTokens = new HashSet<SecurityToken>();
    }

    public string? HonorificTitle { get; private set; }

    public string? FirstName { get; private set; }

    public string? MiddleName { get; private set; }

    public string? LastName { get; private set; }

    public Email Email { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public string PasswordHash { get; private set; }

    public int AccessFailedCount { get; private set; }

    public DateTimeOffset? LastSuccessAccess { get; private set; }

    public bool LockoutEnabled { get; private set; }

    public bool LockedOut => this.LockoutEnabled 
        && this.LockoutEnd is not null
        && this.LockoutEnd > DateTimeOffset.UtcNow;

    public int LockedPeriodInMinutes
        => this.LockoutEnd is null ? 0 : (this.LockoutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes;

    public DateTimeOffset? LockoutEnd { get; private set; }

    public PhoneNumber? PhoneNumber { get; private set; }

    public bool PhoneNumberConfirmed { get; private set; }

    public bool Blocked { get; private set; }

    public string? SecurityStamp { get; private set; }

    public byte[] ConcurrencyStamp { get; private set; }

    public IReadOnlyCollection<Role> Roles => this.roles.ToArray();

    public IReadOnlyCollection<Claim> Claims => this.claims.ToArray();

    public bool HasRole(string roleName)
        => this.roles.Any(r => r.Name == roleName); 

    public User SetPasswordHash(string passowrdHash)
    {
        this.PasswordHash = passowrdHash;
        this.GenerateSecurityStamp();
        return this;
    }

    public User SetUserTitle(string honorificTitle)
    {
        UserValidation.EnsureThat.HasValidTitle(honorificTitle);
        this.HonorificTitle = honorificTitle;
        return this;
    }

    public User SetFirstName(string firstName)
    {
        UserValidation.EnsureThat.HasValidName(firstName);
        this.FirstName = firstName;
        return this;
    }

    public User SetLastName(string lastName)
    {
        UserValidation.EnsureThat.HasValidName(lastName);
        this.LastName = lastName;
        return this;
    }

    public User SetMiddleName(string middleName)
    {
        UserValidation.EnsureThat.HasValidName(middleName);
        this.MiddleName = middleName;
        return this;
    }

    public User SetPhoneNumber(string phoneNumber)
    {
        this.PhoneNumber = phoneNumber;
        return this;
    }

    public User DeletePhoneNumber()
    {
        this.PhoneNumber = null;
        this.PhoneNumberConfirmed = false;
        return this;
    }

    internal User EnableLockout()
    {
        this.LockoutEnabled = true;
        return this;
    }

    internal User DisableLockout()
    {
        this.LockoutEnabled = false;
        this.LockoutEnd = null;
        return this;
    }

    internal User ConfirmEmail()
    {
        this.EmailConfirmed = true;
        return this;
    }

    internal User ConfirmPhoneNumber()
    {
        Ensure.IsNotEmpty<InvalidPhoneNumberException>(this.PhoneNumber?.Value, nameof(this.PhoneNumber));
        this.PhoneNumberConfirmed = true;
        return this;
    }

    internal User RecordSuccessfulAccess()
    {
        this.AccessFailedCount = 0;
        this.LastSuccessAccess = DateTimeOffset.UtcNow;
        if (this.LockoutEnabled)
        {
            this.LockoutEnd = null;
        }

        return this;
    }

    internal User RecordFailedAccess(int maxFailedAttempts, TimeSpan lockoutDuration)
    {
        this.AccessFailedCount++;
        if (this.LockoutEnabled && this.AccessFailedCount > maxFailedAttempts)
        {
            this.LockoutEnd = DateTimeOffset.UtcNow.Add(lockoutDuration);
        }

        return this;
    }

    internal User BlockUser()
    {
        this.Blocked = true;
        this.GenerateSecurityStamp();
        return this;
    }

    internal User UnblockUser()
    {
        this.Blocked = false;
        this.GenerateSecurityStamp();
        return this;
    }

    internal User AddClaim(string type, string value, string valueType)
    {
        this.claims.Add(new Claim(type, value, valueType));
        this.GenerateSecurityStamp();
        return this;
    }

    internal User RemoveClaim(string type)
    {
        var claim = this.claims.FirstOrDefault(c => c.Type == type);
        if (claim != null)
        {
            this.claims.Remove(claim);
            this.GenerateSecurityStamp();
        }

        return this;
    }

    internal User AddRole(Role role)
    {
        this.roles.Add(role);
        this.GenerateSecurityStamp();
        return this;
    }

    internal User RemoveRole(Role role)
    {
        this.roles.Remove(role);
        this.GenerateSecurityStamp();
        return this;
    }

    internal void GenerateSecurityStamp()
    {
        this.SecurityStamp = Ulid.NewUlid().ToString();
    }

    internal void AddToken(SecurityToken token)
    {
        this.securityTokens.Add(token);
    }
}
