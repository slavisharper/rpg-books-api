namespace RpgBooks.Libraries.Module.Application.Commands.Contracts;

/// <summary>
/// Update command contract.
/// </summary>
/// <typeparam name="TId">Type of the identifier passed with the command request.</typeparam>
public interface IUpdateCommand<TId> : ICommand
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    TId Id { get; }

    /// <summary>
    /// Sets the identifier.
    /// </summary>
    /// <param name="id">Identifier value.</param>
    void SetId(TId id);
}
