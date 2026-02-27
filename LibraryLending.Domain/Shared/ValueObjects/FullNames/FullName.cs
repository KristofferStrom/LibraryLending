namespace LibraryLending.Domain.Shared.ValueObjects.FullNames;

public sealed record FullName
{
    public const int NameMaxLength = 100;
    public string FirstName { get; }
    public string LastName { get; }
    public string Value => $"{FirstName} {LastName}";

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(FullNameErrors.FirstNameEmpty);
        if (firstName.Length > NameMaxLength)
            throw new ArgumentException(FullNameErrors.FirstNameTooLong(NameMaxLength));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(FullNameErrors.LastNameEmpty);
        if (lastName.Length > NameMaxLength)
            throw new ArgumentException(FullNameErrors.LastNameTooLong(NameMaxLength));

        return new(firstName.Trim(), lastName.Trim());
    }

    internal static FullName Rehydrate(string firstName, string lastName)
        => new(firstName, lastName);

    public override string ToString() => $"{FirstName} {LastName}";
}
