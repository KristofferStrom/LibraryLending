using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.IntegrationTests.TestInfrastructure;

internal sealed class SqliteInMemoryDb : IAsyncDisposable
{
    private readonly SqliteConnection _connection;

    public SqliteInMemoryDb()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        // Skapa schema en gång
        using var ctx = CreateDbContext();
        ctx.Database.EnsureCreated();
    }

    public LibraryDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        return new LibraryDbContext(options);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}