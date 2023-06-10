namespace RpgBooks.Modules.Identity.Application.Repositories.User.Model;

/// <summary>
/// User details read model.
/// </summary>
public record UserDetailsReadModel
{
    /// <summary>
    /// Gets the user id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets the user email confirmation status.
    /// </summary>
    public bool EmailConfirmed { get; init; }

    /// <summary>
    /// Gets the user honorific title.
    /// </summary>
    public string? HonorificTitle { get; private set; }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets the user middle name.
    /// </summary>
    public string? MiddleName { get; init; }

    /// <summary>
    /// Gets the user last name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Gets the user phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets the user phone number confirmation status.
    /// </summary>
    public bool PhoneNumberConfirmed { get; init; }
}
