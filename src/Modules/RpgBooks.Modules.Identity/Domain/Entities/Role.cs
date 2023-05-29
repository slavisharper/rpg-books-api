namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

public sealed class Role : BaseEntity<int>
{
    private readonly ICollection<Claim> claims;

    public Role(string name)
    {
        this.Name = name;
        this.claims = new HashSet<Claim>();

        this.Validate();
    }

    public string Name { get; private set; }

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
