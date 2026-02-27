using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Members.ValueObjects.MemberNumbers;

public sealed record MemberNumber
{
    public const int MaxLength = 20;
    public string Value { get; }

    private MemberNumber(string value) => Value = value;

    public static Result<MemberNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return MemberNumberErrors.Empty;

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
            MemberNumberErrors.TooLong(MaxLength);

        return new MemberNumber(trimmed);
    }

    internal static MemberNumber Rehydrate(string value) =>
        new(value.Trim());

    public override string ToString() => Value;
}
