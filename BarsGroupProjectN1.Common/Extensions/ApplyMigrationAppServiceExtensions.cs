using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarsGroupProjectN1.Core.Extensions;

public static class ApplyMigrationAppServiceExtensions
{
    public static void ApplyMigration<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        
        dbContext.Database.Migrate();
    }
}