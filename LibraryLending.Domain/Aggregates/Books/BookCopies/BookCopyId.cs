namespace LibraryLending.Domain.Aggregates.Books.BookCopies;

public readonly record struct BookCopyId(Guid Value)
{
    public static BookCopyId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}

