using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Members.ValueObjects.MemberNumbers;

public class MemberNumberErrors
{
    public static Error Empty => new(ErrorType.Validation, "Medlemsnummer får inte vara tomt");
    public static Error TooLong(int maxLength) => new(ErrorType.Validation, $"Medlemsnummer får inte vara längre än {maxLength} tecken");
}
