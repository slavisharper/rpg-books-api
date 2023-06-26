global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;

global using RpgBooks.Libraries.Module.Domain.Entities;
global using RpgBooks.Libraries.Module.Application.Commands;
global using RpgBooks.Libraries.Module.Application.Commands.Contracts;
global using RpgBooks.Libraries.Module.Application.Commands.Extensions;
global using RpgBooks.Libraries.Module.Application.Queries.Contracts;
global using RpgBooks.Libraries.Module.Application.Queries.Extensions;
global using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
global using RpgBooks.Libraries.Module.Application.Results.Contracts;
global using RpgBooks.Libraries.Module.Application.Services;
global using RpgBooks.Libraries.Module.Application.Settings;

global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Security;
global using System.Threading;
global using System.Threading.Tasks;

global using Cysharp.Text;