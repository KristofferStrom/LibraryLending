using LibraryLending.SharedKernel.Results;

public static class IsbnErrors
{
    public static readonly Error Invalid =
        new(ErrorType.Validation, "ISBN måste vara exakt 13 siffror.");
}