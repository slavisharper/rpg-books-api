using RpgBooks.Api.Common.Configuration;
using RpgBooks.Modules.Identity;

WebApplication
    .CreateBuilder(args)
    .AddWebAppConfiguration()
    .AddIdentityModule()
    .Build()
    .UseWebAppConfiguration()
    .Run();