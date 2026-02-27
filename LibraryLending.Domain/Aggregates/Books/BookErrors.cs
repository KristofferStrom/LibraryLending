using LibraryLending.SharedKernel.Results;
namespace LibraryLending.Domain.Aggregates.Books;

public class BookErrors
{
    public static Error TitleEmpty => new(ErrorType.Validation, "Titel får inte vara tomt");
}
