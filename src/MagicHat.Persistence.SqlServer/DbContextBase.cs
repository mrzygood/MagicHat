using Microsoft.EntityFrameworkCore;

namespace MagicHat.Persistence.SqlServer;

public abstract class DbContextBase : DbContext
{
    private readonly string _databaseSchemaName;

    protected DbContextBase(DbContextOptions options, string databaseSchema) 
        : base(options)
    {
        _databaseSchemaName = databaseSchema;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_databaseSchemaName);
    }
}
