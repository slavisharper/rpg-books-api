namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

internal interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);
}
