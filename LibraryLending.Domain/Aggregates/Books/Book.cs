using LibraryLending.Domain.Aggregates.Books.ValueObjects.Isbns;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Books;

public sealed class Book
{
    public Guid Id { get; }
    public string Title { get; private set; }
    public Isbn Isbn { get; private set; }

    private Book(Guid id, string title, Isbn isbn) =>
    (Id, Title, Isbn) = (id, title, isbn);

    public static Result<Book> Create(string title, string isbn)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BookErrors.TitleEmpty;

        var isbnResult = Isbn.Create(isbn);
        if (isbnResult.IsFailure)
            return isbnResult.Error;

        return new Book(Guid.NewGuid(), title.Trim(), isbnResult.Value);
    }

    internal static Book Rehydrate(Guid id, string title, string isbn) =>
    new(id, title, Isbn.Rehydrate(isbn));
}
