namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories.DapperQueries;
internal static class UserDapperQueries
{
    internal const string GetDetailsQuery = @"
        Select 
            Id,
            Email_Value AS Email,
            EmailConfirmed,
            HonorificTitle,
            FirstName,
            MiddleName,
            LastName,
            PhoneNumber_Value AS PhoneNumber,
            PhoneNumberConfirmed
        From IDT_Users
        WHERE Id = @Id";
}
