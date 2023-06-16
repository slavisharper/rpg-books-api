namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Common.Exceptions;
using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;
using RpgBooks.Libraries.Module.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;
using RpgBooks.Modules.Identity.Domain.Validation;

using System;

/// <summary>
/// User entity.
/// </summary>
public class User : BaseEntity<int>, IAggregateRoot, IConcurrentEntity
{
    private readonly ICollection<Claim> claims;
    private readonly ICollection<Role> roles;
    private readonly ICollection<SecurityToken> securityTokens;
    private readonly ICollection<LoginRecord> loginRecords;

    internal User(string email, string passowrdHash)
    {
        this.Email = email;
        this.PasswordHash = passowrdHash;

        this.GenerateSecurityStamp();
        this.ConcurrencyStamp = Array.Empty<byte>();

        this.claims = new HashSet<Claim>();
        this.roles = new HashSet<Role>();
        this.securityTokens = new HashSet<SecurityToken>();
        this.loginRecords = new HashSet<LoginRecord>();
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
        this.loginRecords = new HashSet<LoginRecord>();
    }

    /// <summary>
    /// Gets the user honorific title.
    /// <para>For example: Mr, Mrs, Ms, Dr., etc.</para>
    /// </summary>
    public string? HonorificTitle { get; private set; }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    public string? FirstName { get; private set; }

    /// <summary>
    /// Gets the user middle name.
    /// </summary>
    public string? MiddleName { get; private set; }

    /// <summary>
    /// Gets the user last name also known as surname.
    /// </summary>
    public string? LastName { get; private set; }

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user email is confirmed.
    /// </summary>
    public bool EmailConfirmed { get; private set; }

    /// <summary>
    /// Gets the user password hash.
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets the user access failed count.
    /// </summary>
    public int AccessFailedCount { get; private set; }

    /// <summary>
    /// Gets the user last success access date time in UTC.
    /// </summary>
    public DateTimeOffset? LastSuccessAccess { get; private set; }

    /// <summary>
    /// Get a value indicating whether the user lock out is enabled.
    /// <para>If it is enabled, the user will be locked out for a specific period of time.</para>
    /// <para>After the period of time, the user will be unlocked automatically.</para>
    /// <para>Otherwise the user wont be locked out no-mater how many unsuccessful login attempts are made.</para>
    /// </summary>
    public bool LockoutEnabled { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user is locked out.
    /// </summary>
    public bool LockedOut => this.LockoutEnabled 
        && this.LockoutEnd is not null
        && this.LockoutEnd > DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets the user locked period in minutes.
    /// </summary>
    public int LockedPeriodInMinutes
        => this.LockoutEnd is null
            ? 0
            : (this.LockoutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes;

    /// <summary>
    /// Gets the user lock out end date time in UTC.
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; private set; }

    /// <summary>
    /// Gets the user phone number.
    /// </summary>
    public PhoneNumber? PhoneNumber { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user phone number is confirmed.
    /// </summary>
    public bool PhoneNumberConfirmed { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user is blocked.
    /// <para>If this is true, the user wont be able to login and use any part of the application.</para>
    /// </summary>
    public bool Blocked { get; private set; }

    /// <summary>
    /// Gets the user security stamp.
    /// <para>This is used to invalidate the user authentication token.</para>
    /// </summary>
    public string? SecurityStamp { get; private set; }

    /// <summary>
    /// Gets the user concurrency stamp.
    /// <para>This is used to handle concurrency conflicts when user entity is updated.</para>
    /// </summary>
    public byte[] ConcurrencyStamp { get; private set; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    public IReadOnlyCollection<Role> Roles => this.roles.ToArray();

    /// <summary>
    /// Gets the user claims.
    /// <para>Note that this wont contain the claims that are connected to the user roles.</para>
    /// </summary>
    public IReadOnlyCollection<Claim> Claims => this.claims.ToArray();

    /// <summary>
    /// Gets the user security tokens.
    /// </summary>
    public IReadOnlyCollection<SecurityToken> SecurityTokens => this.securityTokens.ToArray();

    /// <summary>
    /// Gets the user login history records.
    /// </summary>
    public IReadOnlyCollection<LoginRecord> LoginRecords => this.loginRecords.ToArray();

    /// <summary>
    /// Checks whether the user has the given role.
    /// </summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>True if the user has the given role, otherwise false.</returns>
    public bool HasRole(string roleName)
        => this.roles.Any(r => r.Name == roleName); 

    /// <summary>
    /// Set new user password hash.
    /// </summary>
    /// <param name="passowrdHash">New password hash value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetPasswordHash(string passowrdHash)
    {
        this.PasswordHash = passowrdHash;
        this.GenerateSecurityStamp();
        return this;
    }

    /// <summary>
    /// Sets new user honorific title.
    /// </summary>
    /// <param name="honorificTitle">Honorific title value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetUserTitle(string? honorificTitle)
    {
        if (honorificTitle is null)
        {
            this.HonorificTitle = null;
            return this;
        }

        UserValidation.EnsureThat.HasValidTitle(honorificTitle);
        this.HonorificTitle = honorificTitle;
        return this;
    }

    /// <summary>
    /// Set new user first name.
    /// </summary>
    /// <param name="firstName">First name value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetFirstName(string? firstName)
    {
        if (firstName is null)
        {
            this.FirstName = null;
            return this;
        }

        UserValidation.EnsureThat.HasValidName(firstName);
        this.FirstName = firstName;
        return this;
    }

    /// <summary>
    /// Set new user last name.
    /// </summary>
    /// <param name="lastName">Last name value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetLastName(string? lastName)
    {
        if (lastName is null)
        {
            this.LastName = null;
            return this;
        }

        UserValidation.EnsureThat.HasValidName(lastName);
        this.LastName = lastName;
        return this;
    }

    /// <summary>
    /// Set new user middle name.
    /// </summary>
    /// <param name="middleName">User middle name value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetMiddleName(string? middleName)
    {
        if (middleName is null)
        {
            this.MiddleName = null;
            return this;
        }

        UserValidation.EnsureThat.HasValidName(middleName);
        this.MiddleName = middleName;
        return this;
    }

    /// <summary>
    /// Set new user phone number.
    /// <para>Note that this will remove the phone confirmation.</para>
    /// </summary>
    /// <param name="phoneNumber">New phone number value.</param>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
    public User SetPhoneNumber(string phoneNumber)
    {
        if (this.PhoneNumber is not null && phoneNumber == this.PhoneNumber.Value)
        {
            return this;
        }

        this.PhoneNumber = phoneNumber;
        this.PhoneNumberConfirmed = false;
        return this;
    }

    /// <summary>
    /// Deletes the user phone number.
    /// <para>Note that this will remove the phone confirmation.</para>
    /// </summary>
    /// <returns>The same user instance so that multiple calls can be chained.</returns>
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

    internal User RecordSuccessfulAccess(string sessionId, string? ipAddress, string? userAgent)
    {
        this.AccessFailedCount = 0;
        this.LastSuccessAccess = DateTimeOffset.UtcNow;
        if (this.LockoutEnabled)
        {
            this.LockoutEnd = null;
        }

        this.loginRecords.Add(new LoginRecord(sessionId, ipAddress, userAgent));
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
