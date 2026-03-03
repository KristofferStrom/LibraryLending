
namespace LibraryLending.Domain.Shared.Abstractions;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
}
