using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class LoanConfiguration : IEntityTypeConfiguration<LoanEntity>
{
    public void Configure(EntityTypeBuilder<LoanEntity> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MemberId).IsRequired();
        builder.Property(x => x.BookCopyId).IsRequired();

        builder.Property(x => x.LoanDate).IsRequired();
        builder.Property(x => x.DueDate).IsRequired();
        builder.Property(x => x.ReturnedAt);

        builder.HasIndex(x => new { x.MemberId, x.ReturnedAt });
        builder.HasIndex(x => new { x.BookCopyId, x.ReturnedAt });
    }
}
