namespace RpgBooks.Modules.Identity.Application.Queries.UserList;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;

/// <summary>
/// User list query request.
/// </summary>
/// <param name="Id">Id of the user.</param>
public sealed record UserDetailsQuery(int Id) : IQuery;