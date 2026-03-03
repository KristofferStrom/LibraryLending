using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.Domain.Shared.ValueObjects.FullNames;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<MemberEntity>
{
    public void Configure(EntityTypeBuilder<MemberEntity> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MemberNumber)
            .IsRequired()
            .HasMaxLength(MemberNumber.MaxLength);

        builder.HasIndex(x => x.MemberNumber)
            .IsUnique();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(FullName.NameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(FullName.NameMaxLength);

        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}