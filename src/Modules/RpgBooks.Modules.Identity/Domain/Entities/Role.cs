namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// User role entity.
/// </summary>
public sealed class Role : BaseEntity<int>
{
    private readonly ICollection<Claim> claims;

    internal Role(string name)
    {
        this.Name = name;
        this.claims = new HashSet<Claim>();

        this.Validate();
    }

    /// <summary>
    /// Gets the role name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the claims associated with the role.
    /// </summary>
    public IReadOnlyCollection<Claim> Claims => this.claims.ToArray();

    internal Role AddClaim(string type, string value, string valueType)
    {
        this.claims.Add(new Claim(type, value, valueType));
        return this;
    }

    internal Role RemoveClaim(string type)
    {
        var claim = this.claims.FirstOrDefault(c => c.Type == type);
        if (claim != null)
        {
            this.claims.Remove(claim);
        }

        return this;
    }

    private void Validate()
    {
        RoleValidation.EnsureThat.HasValidName(this.Name);
    }
}
