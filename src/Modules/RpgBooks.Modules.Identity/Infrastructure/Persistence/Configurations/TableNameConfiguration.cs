namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Cysharp.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal static class TableNameConfiguration
{
    private const string TableNameFormat = "IDT_{0}s";

    internal static void ConfigureTableName<T>(this EntityTypeBuilder<T> builder)
        where T : class
            => builder.ToTable(ZString.Format(TableNameFormat, typeof(T).Name));

    internal static void ConfigureTableName(this EntityTypeBuilder builder, string name)
        => builder.ToTable(ZString.Format(TableNameFormat, name));
}
