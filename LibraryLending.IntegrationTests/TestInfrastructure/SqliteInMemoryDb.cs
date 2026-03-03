using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.IntegrationTests.TestInfrastructure;


internal sealed class SqliteInMemoryDb : IAsyncDisposable
{
    private readonly SqliteConnection _connection;
    public LibraryDbContext Db { get; }

    public SqliteInMemoryDb()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        Db = new LibraryDbContext(options);

        Db.Database.EnsureCreated();
    }

    public async ValueTask DisposeAsync()
    {
        await Db.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
