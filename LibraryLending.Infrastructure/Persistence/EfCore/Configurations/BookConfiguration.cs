using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.Isbns;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Book.TitleMaxLength);

        builder.Property(x => x.Isbn)
            .IsRequired()
            .HasMaxLength(Isbn.Length)
            .IsFixedLength();

        builder.HasIndex(x => x.Isbn)
            .IsUnique();

        builder.HasMany(x => x.Copies)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
