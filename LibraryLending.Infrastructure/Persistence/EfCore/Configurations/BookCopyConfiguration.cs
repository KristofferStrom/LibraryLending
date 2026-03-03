using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Configurations;


internal sealed class BookCopyConfiguration : IEntityTypeConfiguration<BookCopyEntity>
{
    public void Configure(EntityTypeBuilder<BookCopyEntity> builder)
    {
        builder.ToTable("BookCopies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.Barcode)
            .IsRequired()
            .HasMaxLength(CopyBarcode.MaxLength);

        builder.HasIndex(x => x.Barcode)
            .IsUnique();

        builder.Property(x => x.Status)
            .IsRequired();
    }
}
