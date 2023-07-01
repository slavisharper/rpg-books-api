using RpgBooks.Api.Common.Configuration;
using RpgBooks.Libraries.Module.Application.Logging;
using RpgBooks.Modules.Identity;

using Serilog;

using System.Diagnostics;

LoggingConfiguration.CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");
    WebApplication
        .CreateBuilder(args)
        .AddWebAppConfiguration()
        .AddIdentityModule()
        .Build()
        .UseWebAppConfiguration()
        .Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    Debugger.Break();
}
finally
{
    Log.CloseAndFlush();
}