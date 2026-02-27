namespace LibraryLending.Domain.Aggregates.Books.Isbns;

using LibraryLending.SharedKernel.Results;

public sealed record Isbn
{
    public const int Length = 13;

    public string Value { get; }

    private Isbn(string value) => Value = value;

    public static Result<Isbn> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return IsbnErrors.Invalid;

        var trimmed = value.Trim();

        if (trimmed.Length != Length)
            return IsbnErrors.Invalid;

        if (!trimmed.All(char.IsDigit))
            return IsbnErrors.Invalid;

        return new Isbn(trimmed);
    }

    internal static Isbn Rehydrate(string value) => new(value);

    public override string ToString() => Value;
}

