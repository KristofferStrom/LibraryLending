namespace LibraryLending.Infrastructure.Persistence.EfCore.Entities;

internal sealed class BookCopyEntity
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string Barcode { get; set; } = null!;
    public int Status { get; set; }
    public BookEntity Book { get; set; } = null!;
}
