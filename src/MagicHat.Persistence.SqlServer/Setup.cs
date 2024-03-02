using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MagicHat.Persistence.SqlServer;

public static class Setup
{
    public static IMagicHatConfigurator AddSqlServerPersistence<TContext>(this IMagicHatConfigurator configurator) where TContext : DbContextBase
    {
        configurator.Services.AddDbContext<TContext>(options => {
            // var connectionString = config.GetConnectionString("AppDbContext");

            options.UseSqlServer("connectionString");
        });
        return configurator;
    }
}
