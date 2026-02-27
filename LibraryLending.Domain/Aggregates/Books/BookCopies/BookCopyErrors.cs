using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Books.BookCopies;

public static class BookCopyErrors
{
    public static Error NotAvailable => new(ErrorType.Conflict, "Exemplaret är inte tillgängligt för utlåning.");
    public static Error NotOnLoan => new(ErrorType.Conflict, "Exemplaret är inte utlånat.");
    public static Error BookIdRequired => new(ErrorType.Validation, "Bok-id måste anges.");
}
