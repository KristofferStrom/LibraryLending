using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Books;

public class BookErrors
{
    public static Error TitleEmpty => new(ErrorType.Validation, "Titel får inte vara tomt");

    public static Result<Book> TitleTooLong(int titleMaxLength) => new Error(ErrorType.Validation, $"Titel får inte vara längre än {titleMaxLength} tecken");
}
