namespace LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;

using LibraryLending.SharedKernel.Results;

public sealed record CopyBarcode
{
    public const int MaxLength = 30;
    public string Value { get; }

    private CopyBarcode(string value) => Value = value;

    public static Result<CopyBarcode> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return CopyBarcodeErrors.Empty;

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            return CopyBarcodeErrors.TooLong(MaxLength);

        return new CopyBarcode(trimmed);
    }

    internal static CopyBarcode Rehydrate(string value) => new(value);

    public override string ToString() => Value;
}
