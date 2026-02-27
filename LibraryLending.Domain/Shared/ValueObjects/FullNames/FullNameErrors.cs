namespace LibraryLending.Domain.Shared.ValueObjects.FullNames;

public class FullNameErrors
{
    public const string FirstNameEmpty = "Förnamn får inte vara tomt";
    public const string LastNameEmpty = "Efternamn får inte vara tomt";

    public static string LastNameTooLong(int maxLength) => $"Efternamn får inte vara längre än {maxLength} tecken";
    public static string FirstNameTooLong(int maxLength) => $"Förnamn får inte vara längre än {maxLength} tecken";
}
