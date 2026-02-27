namespace LibraryLending.Domain.Aggregates.Members.ValueObjects.MemberNumbers;

public class MemberNumberErrors
{
    public const string Empty = "Medlemsnummer får inte vara tomt";
    public static string TooLong(int maxLength) => $"Medlemsnummer får inte vara längre än {maxLength} tecken";
}
