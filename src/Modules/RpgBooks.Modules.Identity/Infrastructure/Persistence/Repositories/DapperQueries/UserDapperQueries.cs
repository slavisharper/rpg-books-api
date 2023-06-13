namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories.DapperQueries;
internal static class UserDapperQueries
{
    internal const string GetDetailsQuery = @"
        SELECT 
            [Id],
            [Email_Value] AS [Email],
            [EmailConfirmed],
            [HonorificTitle],
            [FirstName],
            [MiddleName],
            [LastName],
            [PhoneNumber_Value] AS [PhoneNumber],
            [PhoneNumberConfirmed]
        FROM [IDT_Users]
        WHERE Id = @Id";

    internal const string GetActualSecurityToken = @"
        SELECT TOP (1)
	        [Value],
	        [ExpirationTime]
        FROM [IDT_SecurityTokens]
        WHERE UserId = @UserId AND [TokenType_Value] = @TokenType
        ORDER BY [Created] DESC";

    internal const string GetActualSecurityTokenWithSession = @"
        SELECT TOP (1)
	        [Value],
	        [ExpirationTime]
        FROM [IDT_SecurityTokens]
        WHERE UserId = @UserId AND [TokenType_Value] = @TokenType AND [SessionId] = @SessionId
        ORDER BY [Created] DESC";
}
