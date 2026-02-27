using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;


public static class CopyBarcodeErrors
{
    public static Error Empty => new(ErrorType.Validation, "Streckkoden måste anges.");

    public static Error TooLong(int maxLength) => new(ErrorType.Validation, $"Streckkoden får vara max {maxLength} tecken.");
}

