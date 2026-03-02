namespace LibraryLending.Infrastructure.Persistence.EfCore.Entities;

internal sealed class BookEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Isbn { get; set; } = null!;

    public List<BookCopyEntity> Copies { get; set; } = [];
}
