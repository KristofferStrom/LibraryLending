using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Shared.ValueObjects.FullNames;

public class FullNameErrors
{
    public static Error FirstNameEmpty => new(ErrorType.Validation, "Förnamn får inte vara tomt");
    public static Error LastNameEmpty => new(ErrorType.Validation, "Efternamn får inte vara tomt");

    public static Error LastNameTooLong(int maxLength) => new(ErrorType.Validation, $"Efternamn får inte vara längre än {maxLength} tecken");
    public static Error FirstNameTooLong(int maxLength) => new(ErrorType.Validation, $"Förnamn får inte vara längre än {maxLength} tecken");
}
