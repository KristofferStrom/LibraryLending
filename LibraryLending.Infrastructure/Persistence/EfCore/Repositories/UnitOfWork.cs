using LibraryLending.Domain.Shared.Abstractions;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Repositories;

internal sealed class UnitOfWork(LibraryDbContext db) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken ct) =>
        db.SaveChangesAsync(ct);
}
