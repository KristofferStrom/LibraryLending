namespace LibraryLending.Domain.Aggregates.Members.ValueObjects.MemberNumbers;

public sealed record MemberNumber
{
    public const int MaxLength = 20;
    public string Value { get; }

    private MemberNumber(string value) => Value = value;

    public static MemberNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(MemberNumberErrors.Empty);

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            throw new ArgumentException(MemberNumberErrors.TooLong(MaxLength));

        return new MemberNumber(trimmed);
    }

    internal static MemberNumber Rehydrate(string value) =>
        new MemberNumber(value.Trim());

    public override string ToString() => Value;
}
