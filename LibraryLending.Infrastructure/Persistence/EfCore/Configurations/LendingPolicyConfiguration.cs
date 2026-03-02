using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class LendingPolicyConfiguration : IEntityTypeConfiguration<LendingPolicyEntity>
{
    public void Configure(EntityTypeBuilder<LendingPolicyEntity> builder)
    {
        builder.ToTable("LendingPolicies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LoanDays)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => x.IsActive)
            .HasFilter("[IsActive] = 1")
            .IsUnique();
    }
}
