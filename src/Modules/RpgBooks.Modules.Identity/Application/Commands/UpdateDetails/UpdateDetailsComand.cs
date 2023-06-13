namespace RpgBooks.Modules.Identity.Application.Commands.UpdateDetails;

/// <summary>
/// Update user details command request.
/// </summary>
public sealed record UpdateDetailsComand : ICommand
{
    /// <summary>
    /// Gets the user honorific title.
    /// <para>For example: Mr, Mrs, Ms, Dr., etc.</para>
    /// </summary>
    public string? HonorificTitle { get; init; }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets the user middle name.
    /// </summary>
    public string? MiddleName { get; init; }

    /// <summary>
    /// Gets the user last name also known as surname.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Gets the phone number of the user.
    /// </summary>
    public string? PhoneNumber { get; init; }
}
