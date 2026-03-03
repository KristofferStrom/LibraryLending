using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Contexts;

internal sealed class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<BookEntity> Books => Set<BookEntity>();
    public DbSet<BookCopyEntity> BookCopies => Set<BookCopyEntity>();
    public DbSet<MemberEntity> Members => Set<MemberEntity>();
    public DbSet<LoanEntity> Loans => Set<LoanEntity>();
    public DbSet<LendingPolicyEntity> LendingPolicies => Set<LendingPolicyEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
